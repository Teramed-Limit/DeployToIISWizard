using System.Diagnostics;
using DeployToIISWizard.Helper;
using Microsoft.Web.Administration;
using Application = Microsoft.Web.Administration.Application;
using Binding = Microsoft.Web.Administration.Binding;

namespace DeployToIISWizard.IIS;

public static class IISInstaller
{
    // 關閉網站
    public static string StopWebsite(string siteName)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = $"/C iisreset /stop /site \"{siteName}\"";
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return output;
    }

    // 重啟網站
    public static string RestartWebsite(string siteName)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = $"/C iisreset /restart /site \"{siteName}\"";
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return output;
    }

    // 建立網站
    public static void CreateWebsite(string siteName, int portNumber, string hostName)
    {
        // 建立資料夾
        var physicalPath = Path.Combine("C:\\inetpub\\wwwroot", siteName);
        if (!Directory.Exists(physicalPath)) Directory.CreateDirectory(physicalPath);

        // 建立 ServerManager 物件，用於操作 IIS 服務器
        using (ServerManager serverManager = new ServerManager())
        {
            // 使用 Sites.Add 方法新增一個網站，設定名稱、實體路徑、監聽端口等屬性
            Site site = serverManager.Sites.Add(siteName, physicalPath, portNumber);

            // 設定網站的 ServerAutoStart 屬性為 true，表示啟動 IIS 服務器時自動啟動該網站
            site.ServerAutoStart = true;

            // 新增一個網站綁定，設定監聽的主機名稱、監聽的端口號等屬性
            site.Bindings.Add("*:" + portNumber + ":" + hostName, "http");

            // 儲存更改
            serverManager.CommitChanges();
        }
    }

    // 刪除網站
    public static void DeleteWebsite(string siteName)
    {
        var physicalPath = Path.Combine("C:\\inetpub\\wwwroot", siteName);
        if (Directory.Exists(physicalPath)) Utility.DeleteFolder(physicalPath);

        // 建立 ServerManager 物件，用於操作 IIS 服務器
        using (ServerManager serverManager = new ServerManager())
        {
            // 從 Sites 集合中取得指定名稱的網站對象
            Site site = serverManager.Sites[siteName];
            if (site != null)
            {
                // 如果找到了網站對象，就從 Sites 集合中刪除它
                serverManager.Sites.Remove(site);

                // 儲存更改
                serverManager.CommitChanges();
            }
        }
    }

    public static bool IsPortInUse(string siteName, int port)
    {
        using (ServerManager serverManager = new ServerManager())
        {
            foreach (Site site in serverManager.Sites)
            {
                foreach (Binding binding in site.Bindings)
                {
                    if (binding.Protocol is "http" or "https")
                    {
                        if (binding.EndPoint.Port == port && site.Name != siteName)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }


    // 建立虛擬目錄
    public static void CreateVirtualDirectory(string siteName, string virtualDirectoryName, string physicalPath)
    {
        using (ServerManager serverManager = new ServerManager())
        {
            Site site = serverManager.Sites[siteName];
            if (site == null)
            {
                throw new ArgumentException("Site not found", "siteName");
            }

            Application application = site.Applications["/"];
            if (application == null)
            {
                throw new ArgumentException("Application not found", "/");
            }

            VirtualDirectory virtualDirectory =
                application.VirtualDirectories.Add("/" + virtualDirectoryName, physicalPath);
            serverManager.CommitChanges();
        }
    }
}