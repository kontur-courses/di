using Autofac;
using System.Diagnostics;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults.MyStem;

public class MyStem : IDisposable
{
    private bool disposedValue;
    private readonly Process myStemProccess;
    bool isStarted = false;
    private readonly Dictionary<string, WordStat> cache = new();

    public MyStem()
    {
        myStemProccess = new Process();
        myStemProccess.StartInfo.UseShellExecute = false;
        myStemProccess.StartInfo.RedirectStandardInput = true;
        myStemProccess.StartInfo.RedirectStandardOutput = true;
        myStemProccess.StartInfo.RedirectStandardError = true;
        myStemProccess.StartInfo.FileName = "mystem.exe";
        myStemProccess.StartInfo.Arguments = "-nil -e cp866 - -";
    }

    public WordStat AnalyzeWord(string word)
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(MyStem));

        if (cache.TryGetValue(word, out var stat))
            return stat;

        if (!isStarted)
        {
            myStemProccess.Start();
            myStemProccess.BeginErrorReadLine();
            myStemProccess.ErrorDataReceived += HandleErrorData;
            isStarted = true;
        }

        myStemProccess.StandardInput.WriteLine(word);
        var result = myStemProccess.StandardOutput.ReadLine();
        var stemGrSeparation = result.IndexOf('=');
        var grEnd = result.IndexOf('=', stemGrSeparation + 1);
        var part = result[(stemGrSeparation + 1)..grEnd].Split(',')[0];
        stat = new(result[..stemGrSeparation], Enum.Parse<SpeechPart>(part));
        cache[word] = stat;
        return stat;
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<MyStem>().AsSelf().SingleInstance();
    }

    private void HandleErrorData(object sender, DataReceivedEventArgs e)
    {
        throw new InvalidOperationException($"mystem.exe proccess produced an error: {e.Data}");
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            myStemProccess.Dispose();
            disposedValue = true;
        }
    }

    ~MyStem()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
