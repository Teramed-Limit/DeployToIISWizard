using System.Data.SqlClient;
using System.Diagnostics;
using DeployToIISWizard.Helper;
using DeployToIISWizard.IIS;

namespace DeployToIISWizard
{
    public partial class MainForm : Form
    {
        private readonly PowershellExecuter _powershellExecuter;
        private readonly SiteConfigManager _siteConfigManager;
        private const string DeployFolderPath = "C:\\inetpub\\wwwroot";

        public MainForm()
        {
            InitializeComponent();
            _powershellExecuter = new PowershellExecuter(textArea_commandResult);
            _siteConfigManager = SiteConfigManager.Instance;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 檢查目前使用者是否為系統管理員
            if (!Utility.IsUserAdministrator())
            {
                MessageBox.Show("請以系統管理員身分執行此程式");
                Close();
            }

            // 預設 IP 位址為本機 IP 位址
            textBox_ipaddr.Text = Utility.GetLocalIPAddress();

            // 檢查安裝檔案是否存在
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Install", "deploy")))
            {
                btn_createWebSite.Enabled = false;
            }

            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Install", "sql")))
            {
                btn_createSchemaData.Enabled = false;
            }

            // 動態新增ComboBox選項
            foreach (var site in _siteConfigManager.SiteConfig.sites)
            {
                comboBox_sitename.Items.Add(site.name);
            }

            // 預測動態替換字串
            SiteConfigManager.AddOrUpdateParam("siteName", comboBox_sitename.Text);
            SiteConfigManager.AddOrUpdateParam("ip", textBox_ipaddr.Text);
            SiteConfigManager.AddOrUpdateParam("port", textBox_port.Text);
            SiteConfigManager.AddOrUpdateParam("databaseName", textBox_dbname.Text);
            SiteConfigManager.AddOrUpdateParam("serverName", textBox_servername.Text);
            SiteConfigManager.AddOrUpdateParam("dbUserID", textBox_userid.Text);
            SiteConfigManager.AddOrUpdateParam("dbPassword", textBox_password.Text);
        }

        #region Button Event

        private void btn_installIIS_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                var filePath = Path.Combine(Application.StartupPath, "Install", "install_IIS.ps1");
                await _powershellExecuter.RunPowerShell(filePath);
            });
        }

        private void btn_createWebSite_Click(object sender, EventArgs e)
        {
            var result = "";
            result += CheckTextBoxIsNotNull(comboBox_sitename.Text, "請輸入網站名稱\n");
            result += CheckTextBoxIsNotNull(textBox_port.Text, "請輸入網站埠號\n");
            result += CheckTextBoxIsNotNull(textBox_ipaddr.Text, "請輸入網站 IP 位址\n");
            if (!string.IsNullOrEmpty(result))
            {
                MessageBox.Show(result);
                return;
            }

            if (IISInstaller.IsPortInUse(comboBox_sitename.Text, Convert.ToInt32(textBox_port.Text)))
            {
                PrintErrorMessage("Port已經被占用請用另一個");
                return;
            }


            var targetSite = _siteConfigManager.SiteConfig.sites.Find(x => x.name == comboBox_sitename.Text);
            if (targetSite == null)
            {
                PrintErrorMessage("網站名稱不在設定裡");
                return;
            }

            // 建立網站
            PrintMessage("網站建立開始");
            IISInstaller.StopWebsite(comboBox_sitename.Text);
            IISInstaller.DeleteWebsite(comboBox_sitename.Text);
            IISInstaller.CreateWebsite(comboBox_sitename.Text, Convert.ToInt32(textBox_port.Text), textBox_ipaddr.Text);
            // 建立虛擬目錄
            foreach (var virtualDirectory in targetSite.virtualDirectory)
            {
                PrintMessage($"建立虛擬目錄: 名稱:{virtualDirectory.name}, 路徑:{virtualDirectory.path}");
                IISInstaller.CreateVirtualDirectory(
                    comboBox_sitename.Text,
                    virtualDirectory.name,
                    virtualDirectory.path);
            }

            // 部屬資料
            Task.Run(async () =>
            {
                PrintMessage("部屬中......");
                var sitePhysicalPath = Path.Combine(DeployFolderPath, comboBox_sitename.Text);
                var deployPath = Path.Combine(Application.StartupPath, "Install", "deploy");
                await Utility.CopyFolderAsync(deployPath, sitePhysicalPath);
                PrintMessage("網站建立完成");

                foreach (var extraResource in targetSite.extraResources)
                {
                    var from = Path.Combine(Application.StartupPath, "Install", extraResource.from);
                    var to = extraResource.to;
                    await Utility.CopyFolderAsync(from, to);
                }

                // 安裝 MSI
                PrintMessage("安裝 MSI");
                foreach (var startInfo in targetSite.installList
                             .Select(fileName =>
                                 Path.Combine(Application.StartupPath, "Install", "Installer", fileName))
                             .Select(msiPath => new ProcessStartInfo
                             {
                                 FileName = "msiexec.exe",
                                 Arguments = $"/i \"{msiPath}\"",
                                 Verb = "runas"
                             }))
                {
                    // 啟動 MSI 安裝程式
                    try
                    {
                        using (var process = Process.Start(startInfo))
                        {
                            await process?.WaitForExitAsync()!;
                            PrintMessage($"安裝已完成 {startInfo.Arguments}");
                        }
                    }
                    catch (Exception ex)
                    {
                        PrintErrorMessage($"安裝失敗：{ex.Message}");
                    }
                }
            });
        }

        private void btn_installPACS_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                // 設定
                var argList = new Dictionary<string, object>
                {
                    { "ServerName", textBox_servername.Text },
                    { "DatabaseName", textBox_dbname.Text },
                    { "DBUserID", textBox_userid.Text },
                    { "DBPassword", textBox_password.Text },
                };
                var filePath = Path.Combine(Application.StartupPath, "Install", "install_pacs.ps1");
                await _powershellExecuter.RunPowerShell(filePath, argList);
            });
        }

        private void btn_createSchemaData_Click(object sender, EventArgs e)
        {
            var filePath = Path.Combine(Application.StartupPath, "Install", "create_schema_data.ps1");
            _powershellExecuter.RunPowerShell(filePath);
        }

        private void btn_configuration_Click(object sender, EventArgs e)
        {
            var targetSite = _siteConfigManager.SiteConfig.sites.Find(x => x.name == comboBox_sitename.Text);
            if (targetSite == null)
            {
                PrintErrorMessage("網站名稱不在設定裡");
                return;
            }

            // 部屬資料
            Task.Run(async () =>
            {
                // JS前端api endpoint更換
                PrintMessage("設定初始化中......");
                var filePath = Path.Combine(Application.StartupPath, "Install", "replace_api_ip_port.ps1");
                var argList = new Dictionary<string, object>
                {
                    { "SiteName", comboBox_sitename.Text },
                    { "SitePort", Convert.ToInt32(textBox_port.Text) },
                    { "IpAddress", textBox_ipaddr.Text },
                    { "TargetDir", targetSite.config.frontEndJSDir },
                };
                await _powershellExecuter.RunPowerShell(filePath, argList);

                // Web.config 設定
                var configPath = targetSite.config.backendConfigPath;
                if (!File.Exists(configPath))
                {
                    PrintErrorMessage($"設定檔案遺失: {configPath}");
                    return;
                }

                foreach (var param in targetSite.config.@params)
                {
                    var value = SiteConfigManager.Parse(param.value);
                    SiteConfigManager.UpdateWebConfigSettings(param.key, value, configPath);
                    PrintMessage("替換設定: " + param.key + " = " + value);
                }

                PrintMessage("設定完成");

                PrintMessage("重啟網站中....");
                IISInstaller.RestartWebsite(comboBox_sitename.Text);
                PrintMessage("重啟完成");
            });
        }

        private void btn_testdb_Click(object sender, EventArgs e)
        {
            var result = "";
            result += CheckTextBoxIsNotNull(textBox_servername.Text, "請輸入主機名稱\n");
            result += CheckTextBoxIsNotNull(textBox_dbname.Text, "請輸入資料庫名稱\n");
            result += CheckTextBoxIsNotNull(textBox_userid.Text, "請輸入登入者\n");
            result += CheckTextBoxIsNotNull(textBox_password.Text, "請輸入資料庫密碼\n");
            if (!string.IsNullOrEmpty(result))
            {
                MessageBox.Show(result);
                return;
            }

            try
            {
                int timeoutSeconds = 3;
                string connectionString =
                    $"Data Source={textBox_servername.Text};Initial Catalog={textBox_dbname.Text};User ID={textBox_userid.Text};Password={textBox_password.Text};Connection Timeout={timeoutSeconds};";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                }

                PrintMessage("資料庫連線成功");
                btn_configuration.Enabled = true;
                btn_installPACS.Enabled = true;
            }
            catch (SqlException exception)
            {
                PrintErrorMessage(exception.Message);
            }
        }

        #endregion

        private string CheckTextBoxIsNotNull(string text, string errorMsg)
        {
            return string.IsNullOrEmpty(text) ? errorMsg : "";
        }

        #region Print Message

        private void PrintMessage(string message)
        {
            textArea_commandResult.Invoke((() =>
                textArea_commandResult.AppendText(message + Environment.NewLine)));
        }

        private void PrintErrorMessage(string message)
        {
            textArea_commandResult.Invoke(() =>
            {
                textArea_commandResult.SelectionColor = Color.Red; // 可以使用紅色來突顯錯誤訊息
                textArea_commandResult.AppendText($"錯誤: {message}{Environment.NewLine}");
                textArea_commandResult.SelectionColor = textArea_commandResult.ForeColor;
            });
        }

        #endregion

        #region TextBox Event

        private void comboBox_sitename_TextChanged(object sender, EventArgs e)
        {
            SiteConfigManager.AddOrUpdateParam("siteName", comboBox_sitename.Text);
            // 更改dbname
            textBox_dbname.Text =
                _siteConfigManager.SiteConfig.sites.Find(x => x.name == comboBox_sitename.Text)?.dbName;
        }

        private void textBox_ipaddr_TextChanged(object sender, EventArgs e)
        {
            SiteConfigManager.AddOrUpdateParam("ip", textBox_ipaddr.Text);
        }

        private void textBox_port_TextChanged(object sender, EventArgs e)
        {
            SiteConfigManager.AddOrUpdateParam("port", textBox_port.Text);
        }

        private void textBox_dbname_TextChanged(object sender, EventArgs e)
        {
            SiteConfigManager.AddOrUpdateParam("databaseName", textBox_dbname.Text);
        }

        private void textBox_servername_TextChanged(object sender, EventArgs e)
        {
            SiteConfigManager.AddOrUpdateParam("serverName", textBox_servername.Text);
        }

        private void textBox_userid_TextChanged(object sender, EventArgs e)
        {
            SiteConfigManager.AddOrUpdateParam("dbUserID", textBox_userid.Text);
        }

        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
            SiteConfigManager.AddOrUpdateParam("dbPassword", textBox_password.Text);
        }

        #endregion

        #region Select Folder Event

        private void btn_selectDeployDir_Click(object sender, EventArgs e)
        {
            // 建立 FolderBrowserDialog 元件
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                // 設定對話框的標題
                folderBrowserDialog.Description = "請選擇資料夾：";
                folderBrowserDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Install");

                // 顯示對話框，並判斷是否選擇了資料夾
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // 取得選擇的資料夾路徑
                    string folderPath = folderBrowserDialog.SelectedPath;

                    // 將路徑顯示在文字框中
                    Utility.CopyFolder(folderPath, Path.Combine(Application.StartupPath, "Install", "deploy"));
                }
            }

            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Install", "deploy")))
            {
                btn_createWebSite.Enabled = true;
            }
        }

        private void btn_selectSqlDir_Click(object sender, EventArgs e)
        {
            // 建立 FolderBrowserDialog 元件
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                // 設定對話框的標題
                folderBrowserDialog.Description = "請選擇資料夾：";
                folderBrowserDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Install");

                // 顯示對話框，並判斷是否選擇了資料夾
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // 取得選擇的資料夾路徑
                    string folderPath = folderBrowserDialog.SelectedPath;

                    // 將路徑顯示在文字框中
                    Utility.CopyFolder(folderPath, Path.Combine(Application.StartupPath, "Install", "sql"));
                }
            }

            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Install", "sql")))
            {
                btn_createSchemaData.Enabled = true;
            }
        }

        #endregion
    }
}