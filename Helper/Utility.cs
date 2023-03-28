using System.Configuration;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Xml;

namespace DeployToIISWizard.Helper;

public static class Utility
{
    // 判斷目前使用者是否為系統管理員
    public static bool IsUserAdministrator()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    // 取得本機 IP 位址
    public static string GetLocalIPAddress()
    {
        // 獲取所有本機網路介面的資訊
        IPInterfaceProperties? ipProps = NetworkInterface.GetAllNetworkInterfaces()
            .Where(ni => ni.OperationalStatus == OperationalStatus.Up)
            .Select(ni => ni.GetIPProperties())
            .FirstOrDefault();

        if (ipProps != null)
        {
            IPAddress? ipAddress = ipProps.UnicastAddresses
                .Where(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(addr => addr.Address)
                .FirstOrDefault();

            if (ipAddress != null)
            {
                return ipAddress.ToString();
            }
        }

        return "";
    }

    public static async Task CopyFolderAsync(string sourceFolder, string destinationFolder)
    {
        await Task.Run(() => CopyFolder(sourceFolder, destinationFolder));
    }

    public static void CopyFolder(string sourceFolder, string destinationFolder)
    {
        if (!Directory.Exists(sourceFolder))
        {
            throw new DirectoryNotFoundException("Source folder not found");
        }

        if (!Directory.Exists(destinationFolder))
        {
            Directory.CreateDirectory(destinationFolder);
        }

        foreach (string file in Directory.GetFiles(sourceFolder))
        {
            string fileName = Path.GetFileName(file);
            string destinationFile = Path.Combine(destinationFolder, fileName);
            File.Copy(file, destinationFile, true);
        }

        foreach (string folder in Directory.GetDirectories(sourceFolder))
        {
            string folderName = Path.GetFileName(folder);
            string destinationFolderName = Path.Combine(destinationFolder, folderName);
            CopyFolder(folder, destinationFolderName);
        }
    }

    public static void DeleteFolder(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            return;
        }

        string[] files = Directory.GetFiles(folderPath);
        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        string[] folders = Directory.GetDirectories(folderPath);
        foreach (string folder in folders)
        {
            DeleteFolder(folder);
        }

        Directory.Delete(folderPath, false);
    }
}