using System.Collections.Concurrent;
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

    public IEnumerable<string> Analyze(IEnumerable<string> sourceWords)
    {
        using var myStem = StartProcess();
        var results = new ConcurrentQueue<string>();
        using var task = RunOutputReaderTask(myStem, results);

        foreach (var sourceWord in sourceWords)
        {
            myStem.StandardInput.WriteLine(sourceWord);
            myStem.StandardInput.Flush();
            while (results.TryDequeue(out var result))
                yield return result;
        }

        myStem.StandardInput.Close();
        task.GetAwaiter().GetResult();
        myStem.Kill();

        while (results.TryDequeue(out var result))
            yield return result;
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

    private static Task RunOutputReaderTask(Process process, ConcurrentQueue<string> outResults) =>
        Task.Run(
            async () =>
            {
                while (!process.StandardOutput.EndOfStream)
                {
                    var line = await process.StandardOutput.ReadLineAsync().ConfigureAwait(false);
                    if (line is not null)
                        outResults.Enqueue(line);
                }
            }
        );
}