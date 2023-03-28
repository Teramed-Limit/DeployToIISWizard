namespace DeployToIISWizard
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textArea_commandResult = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBox_sitename = new System.Windows.Forms.ComboBox();
            this.btn_selectSqlDir = new System.Windows.Forms.Button();
            this.btn_selectDeployDir = new System.Windows.Forms.Button();
            this.btn_testdb = new System.Windows.Forms.Button();
            this.label_pwd = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label_userid = new System.Windows.Forms.Label();
            this.textBox_userid = new System.Windows.Forms.TextBox();
            this.textBox_servername = new System.Windows.Forms.TextBox();
            this.textBox_dbname = new System.Windows.Forms.TextBox();
            this.label_servername = new System.Windows.Forms.Label();
            this.label_dbname = new System.Windows.Forms.Label();
            this.btn_createSchemaData = new System.Windows.Forms.Button();
            this.btn_configuration = new System.Windows.Forms.Button();
            this.btn_installPACS = new System.Windows.Forms.Button();
            this.lable_port = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_ipaddr = new System.Windows.Forms.TextBox();
            this.label_ipaddr = new System.Windows.Forms.Label();
            this.label_sitename = new System.Windows.Forms.Label();
            this.btn_createWebSite = new System.Windows.Forms.Button();
            this.btn_installIIS = new System.Windows.Forms.Button();
            this.tc_InstallIIS = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.tc_InstallIIS.SuspendLayout();
            this.SuspendLayout();
            // 
            // textArea_commandResult
            // 
            this.textArea_commandResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textArea_commandResult.Location = new System.Drawing.Point(0, 285);
            this.textArea_commandResult.Name = "textArea_commandResult";
            this.textArea_commandResult.Size = new System.Drawing.Size(800, 270);
            this.textArea_commandResult.TabIndex = 5;
            this.textArea_commandResult.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBox_sitename);
            this.tabPage1.Controls.Add(this.btn_selectSqlDir);
            this.tabPage1.Controls.Add(this.btn_selectDeployDir);
            this.tabPage1.Controls.Add(this.btn_testdb);
            this.tabPage1.Controls.Add(this.label_pwd);
            this.tabPage1.Controls.Add(this.textBox_password);
            this.tabPage1.Controls.Add(this.label_userid);
            this.tabPage1.Controls.Add(this.textBox_userid);
            this.tabPage1.Controls.Add(this.textBox_servername);
            this.tabPage1.Controls.Add(this.textBox_dbname);
            this.tabPage1.Controls.Add(this.label_servername);
            this.tabPage1.Controls.Add(this.label_dbname);
            this.tabPage1.Controls.Add(this.btn_createSchemaData);
            this.tabPage1.Controls.Add(this.btn_configuration);
            this.tabPage1.Controls.Add(this.btn_installPACS);
            this.tabPage1.Controls.Add(this.lable_port);
            this.tabPage1.Controls.Add(this.textBox_port);
            this.tabPage1.Controls.Add(this.textBox_ipaddr);
            this.tabPage1.Controls.Add(this.label_ipaddr);
            this.tabPage1.Controls.Add(this.label_sitename);
            this.tabPage1.Controls.Add(this.btn_createWebSite);
            this.tabPage1.Controls.Add(this.btn_installIIS);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "安裝";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox_sitename
            // 
            this.comboBox_sitename.FormattingEnabled = true;
            this.comboBox_sitename.Location = new System.Drawing.Point(94, 8);
            this.comboBox_sitename.Name = "comboBox_sitename";
            this.comboBox_sitename.Size = new System.Drawing.Size(100, 23);
            this.comboBox_sitename.TabIndex = 32;
            this.comboBox_sitename.TextChanged += new System.EventHandler(this.comboBox_sitename_TextChanged);
            // 
            // btn_selectSqlDir
            // 
            this.btn_selectSqlDir.Location = new System.Drawing.Point(321, 93);
            this.btn_selectSqlDir.Name = "btn_selectSqlDir";
            this.btn_selectSqlDir.Size = new System.Drawing.Size(26, 23);
            this.btn_selectSqlDir.TabIndex = 31;
            this.btn_selectSqlDir.Text = "...";
            this.btn_selectSqlDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_selectSqlDir.UseVisualStyleBackColor = true;
            this.btn_selectSqlDir.Click += new System.EventHandler(this.btn_selectSqlDir_Click);
            // 
            // btn_selectDeployDir
            // 
            this.btn_selectDeployDir.Location = new System.Drawing.Point(321, 64);
            this.btn_selectDeployDir.Name = "btn_selectDeployDir";
            this.btn_selectDeployDir.Size = new System.Drawing.Size(26, 23);
            this.btn_selectDeployDir.TabIndex = 30;
            this.btn_selectDeployDir.Text = "...";
            this.btn_selectDeployDir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_selectDeployDir.UseVisualStyleBackColor = true;
            this.btn_selectDeployDir.Click += new System.EventHandler(this.btn_selectDeployDir_Click);
            // 
            // btn_testdb
            // 
            this.btn_testdb.Location = new System.Drawing.Point(200, 7);
            this.btn_testdb.Name = "btn_testdb";
            this.btn_testdb.Size = new System.Drawing.Size(115, 23);
            this.btn_testdb.TabIndex = 29;
            this.btn_testdb.Text = "測試資料庫連線";
            this.btn_testdb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_testdb.UseVisualStyleBackColor = true;
            this.btn_testdb.Click += new System.EventHandler(this.btn_testdb_Click);
            // 
            // label_pwd
            // 
            this.label_pwd.AutoSize = true;
            this.label_pwd.Location = new System.Drawing.Point(8, 209);
            this.label_pwd.Name = "label_pwd";
            this.label_pwd.Size = new System.Drawing.Size(60, 15);
            this.label_pwd.TabIndex = 28;
            this.label_pwd.Text = "Password";
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(94, 206);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(100, 23);
            this.textBox_password.TabIndex = 27;
            this.textBox_password.Text = "admin";
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_password_TextChanged);
            // 
            // label_userid
            // 
            this.label_userid.AutoSize = true;
            this.label_userid.Location = new System.Drawing.Point(8, 180);
            this.label_userid.Name = "label_userid";
            this.label_userid.Size = new System.Drawing.Size(46, 15);
            this.label_userid.TabIndex = 26;
            this.label_userid.Text = "User Id";
            // 
            // textBox_userid
            // 
            this.textBox_userid.Location = new System.Drawing.Point(94, 177);
            this.textBox_userid.Name = "textBox_userid";
            this.textBox_userid.Size = new System.Drawing.Size(100, 23);
            this.textBox_userid.TabIndex = 25;
            this.textBox_userid.Text = "sa";
            this.textBox_userid.TextChanged += new System.EventHandler(this.textBox_userid_TextChanged);
            // 
            // textBox_servername
            // 
            this.textBox_servername.Location = new System.Drawing.Point(94, 148);
            this.textBox_servername.Name = "textBox_servername";
            this.textBox_servername.Size = new System.Drawing.Size(100, 23);
            this.textBox_servername.TabIndex = 23;
            this.textBox_servername.Text = "127.0.0.1";
            this.textBox_servername.TextChanged += new System.EventHandler(this.textBox_servername_TextChanged);
            // 
            // textBox_dbname
            // 
            this.textBox_dbname.Location = new System.Drawing.Point(94, 119);
            this.textBox_dbname.Name = "textBox_dbname";
            this.textBox_dbname.Size = new System.Drawing.Size(100, 23);
            this.textBox_dbname.TabIndex = 21;
            this.textBox_dbname.TextChanged += new System.EventHandler(this.textBox_dbname_TextChanged);
            // 
            // label_servername
            // 
            this.label_servername.AutoSize = true;
            this.label_servername.Location = new System.Drawing.Point(8, 151);
            this.label_servername.Name = "label_servername";
            this.label_servername.Size = new System.Drawing.Size(80, 15);
            this.label_servername.TabIndex = 24;
            this.label_servername.Text = "Server Name";
            // 
            // label_dbname
            // 
            this.label_dbname.AutoSize = true;
            this.label_dbname.Location = new System.Drawing.Point(8, 122);
            this.label_dbname.Name = "label_dbname";
            this.label_dbname.Size = new System.Drawing.Size(61, 15);
            this.label_dbname.TabIndex = 22;
            this.label_dbname.Text = "DB Name";
            // 
            // btn_createSchemaData
            // 
            this.btn_createSchemaData.Location = new System.Drawing.Point(200, 93);
            this.btn_createSchemaData.Name = "btn_createSchemaData";
            this.btn_createSchemaData.Size = new System.Drawing.Size(115, 23);
            this.btn_createSchemaData.TabIndex = 20;
            this.btn_createSchemaData.Text = "3. 建立資料表";
            this.btn_createSchemaData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_createSchemaData.UseVisualStyleBackColor = true;
            this.btn_createSchemaData.Click += new System.EventHandler(this.btn_createSchemaData_Click);
            // 
            // btn_configuration
            // 
            this.btn_configuration.Enabled = false;
            this.btn_configuration.Location = new System.Drawing.Point(200, 150);
            this.btn_configuration.Name = "btn_configuration";
            this.btn_configuration.Size = new System.Drawing.Size(115, 23);
            this.btn_configuration.TabIndex = 19;
            this.btn_configuration.Text = "5. 更新前端與後端設定檔";
            this.btn_configuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_configuration.UseVisualStyleBackColor = true;
            this.btn_configuration.Click += new System.EventHandler(this.btn_configuration_Click);
            // 
            // btn_installPACS
            // 
            this.btn_installPACS.Enabled = false;
            this.btn_installPACS.Location = new System.Drawing.Point(200, 122);
            this.btn_installPACS.Name = "btn_installPACS";
            this.btn_installPACS.Size = new System.Drawing.Size(115, 23);
            this.btn_installPACS.TabIndex = 17;
            this.btn_installPACS.Text = "4. 安裝PACS";
            this.btn_installPACS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_installPACS.UseVisualStyleBackColor = true;
            this.btn_installPACS.Click += new System.EventHandler(this.btn_installPACS_Click);
            // 
            // lable_port
            // 
            this.lable_port.AutoSize = true;
            this.lable_port.Location = new System.Drawing.Point(8, 68);
            this.lable_port.Name = "lable_port";
            this.lable_port.Size = new System.Drawing.Size(30, 15);
            this.lable_port.TabIndex = 16;
            this.lable_port.Text = "Port";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(94, 65);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(100, 23);
            this.textBox_port.TabIndex = 15;
            this.textBox_port.Text = "8080";
            this.textBox_port.TextChanged += new System.EventHandler(this.textBox_port_TextChanged);
            // 
            // textBox_ipaddr
            // 
            this.textBox_ipaddr.Location = new System.Drawing.Point(94, 36);
            this.textBox_ipaddr.Name = "textBox_ipaddr";
            this.textBox_ipaddr.Size = new System.Drawing.Size(100, 23);
            this.textBox_ipaddr.TabIndex = 13;
            this.textBox_ipaddr.TextChanged += new System.EventHandler(this.textBox_ipaddr_TextChanged);
            // 
            // label_ipaddr
            // 
            this.label_ipaddr.AutoSize = true;
            this.label_ipaddr.Location = new System.Drawing.Point(8, 39);
            this.label_ipaddr.Name = "label_ipaddr";
            this.label_ipaddr.Size = new System.Drawing.Size(65, 15);
            this.label_ipaddr.TabIndex = 14;
            this.label_ipaddr.Text = "IP Address";
            // 
            // label_sitename
            // 
            this.label_sitename.AutoSize = true;
            this.label_sitename.Location = new System.Drawing.Point(8, 10);
            this.label_sitename.Name = "label_sitename";
            this.label_sitename.Size = new System.Drawing.Size(66, 15);
            this.label_sitename.TabIndex = 12;
            this.label_sitename.Text = "Site Name";
            // 
            // btn_createWebSite
            // 
            this.btn_createWebSite.Location = new System.Drawing.Point(200, 64);
            this.btn_createWebSite.Name = "btn_createWebSite";
            this.btn_createWebSite.Size = new System.Drawing.Size(115, 23);
            this.btn_createWebSite.TabIndex = 10;
            this.btn_createWebSite.Text = "2. 建立網站";
            this.btn_createWebSite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_createWebSite.UseVisualStyleBackColor = true;
            this.btn_createWebSite.Click += new System.EventHandler(this.btn_createWebSite_Click);
            // 
            // btn_installIIS
            // 
            this.btn_installIIS.Location = new System.Drawing.Point(200, 35);
            this.btn_installIIS.Name = "btn_installIIS";
            this.btn_installIIS.Size = new System.Drawing.Size(115, 23);
            this.btn_installIIS.TabIndex = 2;
            this.btn_installIIS.Text = "1. 安裝IIS";
            this.btn_installIIS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_installIIS.UseVisualStyleBackColor = true;
            this.btn_installIIS.Click += new System.EventHandler(this.btn_installIIS_Click);
            // 
            // tc_InstallIIS
            // 
            this.tc_InstallIIS.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tc_InstallIIS.Controls.Add(this.tabPage1);
            this.tc_InstallIIS.Dock = System.Windows.Forms.DockStyle.Top;
            this.tc_InstallIIS.Location = new System.Drawing.Point(0, 0);
            this.tc_InstallIIS.Multiline = true;
            this.tc_InstallIIS.Name = "tc_InstallIIS";
            this.tc_InstallIIS.SelectedIndex = 0;
            this.tc_InstallIIS.Size = new System.Drawing.Size(800, 285);
            this.tc_InstallIIS.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 555);
            this.Controls.Add(this.textArea_commandResult);
            this.Controls.Add(this.tc_InstallIIS);
            this.Name = "MainForm";
            this.Text = "Web App Install Wizard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tc_InstallIIS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private RichTextBox textArea_commandResult;
        private TabPage tabPage1;
        private Button btn_createSchemaData;
        private Button btn_configuration;
        private Button btn_installPACS;
        private Label lable_port;
        private TextBox textBox_port;
        private TextBox textBox_ipaddr;
        private Label label_ipaddr;
        private Label label_sitename;
        private Button btn_createWebSite;
        private Button btn_installIIS;
        private TabControl tc_InstallIIS;
        private Label label_pwd;
        private TextBox textBox_password;
        private Label label_userid;
        private TextBox textBox_userid;
        private TextBox textBox_servername;
        private TextBox textBox_dbname;
        private Label label_servername;
        private Label label_dbname;
        private Button btn_testdb;
        private Button btn_selectSqlDir;
        private Button btn_selectDeployDir;
        private ComboBox comboBox_sitename;
    }
}