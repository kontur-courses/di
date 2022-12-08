using System.Diagnostics;
using System.Text;
using MyStemWrapper.Domain.Settings;
using MyStemWrapper.Infrastructure;

namespace MyStemWrapper;

public class MyStem
{
    private readonly MyStemSettings _myStemSettings;

    public MyStem(MyStemSettings myStemSettings)
    {
        _myStemSettings = myStemSettings;
    }

    public string Analyze(string source)
    {
        using var myStem = StartProcess();

        myStem.StandardInput.Write(source);
        myStem.StandardInput.Flush();
        myStem.StandardInput.Close();

        var result = myStem.StandardOutput.ReadToEnd();
        myStem.WaitForExit();

        return result;
    }

    private Process StartProcess()
    {
        if (!_myStemSettings.CheckCorrectAppPath())
            throw new FileNotFoundException($"File not found, invalid path: {_myStemSettings.MyStemAppPath}");

        return Process.Start(
            new ProcessStartInfo
            {
                FileName = _myStemSettings.MyStemAppPath,
                Arguments = _myStemSettings.GetLaunchArgsToString(),
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardInputEncoding = Encoding.UTF8
            }
        )!;
    }
}