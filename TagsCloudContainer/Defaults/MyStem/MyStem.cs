using System.Diagnostics;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults.MyStem;

public class MyStem : ISingletonService, IDisposable
{
    private static bool disposedValue;
    private static readonly ProcessStartInfo startInfo = new()
    {
        UseShellExecute = false,
        RedirectStandardInput = true,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        FileName = "mystem.exe",
        Arguments = "-nil -e cp866 - -"
    };
    private readonly Process myStemProccess;
    private bool isStarted = false;
    private readonly Dictionary<string, WordStat> cache = new();
    private readonly Dictionary<string, WordStat[]> complexWordCache = new();

    public MyStem()
    {
        myStemProccess = new()
        {
            StartInfo = startInfo
        };
    }

    public WordStat? AnalyzeWord(string word)
    {
        if (disposedValue)
            throw new ObjectDisposedException(nameof(MyStem));

        if (word == null)
            return null;

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

        var stats = ParseWordStats(ReadAllLines(myStemProccess.StandardOutput));
        stat = stats.Length > 0 ? stats[0] : null;
        if (stat != null)
        {
            cache[word] = stat;
            cache[stat.Stem] = stat;
        }

        return stat;
    }

    private static IEnumerable<string> ReadAllLines(StreamReader reader)
    {
        yield return reader.ReadLine()!;
        while (reader.Peek() != -1)
        {
            yield return reader.ReadLine()!;
        }
    }

    private static WordStat[] ParseWordStats(IEnumerable<string> lines)
    {
        return lines.Select(ParseWordStat)
            .Where(x => x != null)
            .OrderByDescending(x => x!.Stem.Length)
            .ToArray()!;
    }

    private static WordStat? ParseWordStat(string wordStat)
    {
        if (wordStat.EndsWith("??"))
            return null;
        var stemGrSeparation = wordStat.IndexOf('=');
        var grEnd = wordStat.IndexOf('=', stemGrSeparation + 1);
        var part = wordStat[(stemGrSeparation + 1)..grEnd].Split(',')[0];
        var stem = wordStat[..stemGrSeparation].TrimEnd('?');
        return new(stem, Enum.Parse<SpeechPart>(part));
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
