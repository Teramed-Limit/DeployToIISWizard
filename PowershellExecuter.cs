using System.Diagnostics;

namespace DeployToIISWizard;

public class PowershellExecuter
{
    private readonly RichTextBox _textAreaCommandResult;

    public PowershellExecuter(RichTextBox textAreaCommandResult)
    {
        _textAreaCommandResult = textAreaCommandResult;
    }

    // 建立 ProcessStartInfo 物件，設定要執行的 PowerShell 指令
    public async Task RunPowerShell(string scriptPath, Dictionary<string, object>? parameters = null)
    {
        string arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\"";
        if (parameters != null)
            // 將字典中的參數添加到命令行參數中
            foreach (var parameter in parameters)
            {
                switch (parameter.Value)
                {
                    case string:
                        arguments += $" -{parameter.Key} \"{parameter.Value}\"";
                        break;
                    case int:
                        arguments += $" -{parameter.Key} {parameter.Value}";
                        break;
                }
            }

        var startInfo = new ProcessStartInfo()
        {
            FileName = "powershell.exe",
            Arguments = arguments,
            UseShellExecute = false,
            Verb = "runas", // 以系統管理員身份運行
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
        };

        var process = new Process() { StartInfo = startInfo };
        process.OutputDataReceived += OutputHandler;
        process.ErrorDataReceived += ErrorHandler;
        process.Exited += ExitedHandler;
        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.EnableRaisingEvents = true;
        await process.WaitForExitAsync(); // 等待前一個腳本執行完成
    }

    private void OutputHandler(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            _textAreaCommandResult.Invoke((() =>
                _textAreaCommandResult.AppendText(e.Data + Environment.NewLine)));
        }
    }

    private void ErrorHandler(object sender, DataReceivedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Data))
        {
            _textAreaCommandResult.Invoke(() =>
            {
                _textAreaCommandResult.SelectionColor = Color.Red; // 可以使用紅色來突顯錯誤訊息
                _textAreaCommandResult.AppendText($"錯誤: {e.Data}{Environment.NewLine}");
                _textAreaCommandResult.SelectionColor = _textAreaCommandResult.ForeColor;
            });
        }
    }

    private void ExitedHandler(object sender, EventArgs e)
    {
        // 在這裡執行你希望在腳本執行完成後執行的操作
    }
}