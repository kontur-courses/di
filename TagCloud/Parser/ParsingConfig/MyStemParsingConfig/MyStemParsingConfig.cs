using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.IO.Compression;

namespace TagCloud.Parser.ParsingConfig.MyStemParsingConfig;

public abstract class MyStemParsingConfig : IParsingConfig
{
    public abstract bool IsWordExcluded(string word);
    
    private const string MyStemUrl = "http://download.cdn.yandex.net/mystem/mystem-3.1-win-64bit.zip";
    private static readonly string WorkingDirectory = Environment.CurrentDirectory;
    private readonly Regex wordTypeRegex = new("=([a-zA-Z]{1,6})[=,]");
    private readonly Process myStem;
    
    protected MyStemParsingConfig()
    {
        if (TryFindMyStem(out var myStemPath))
        {
            myStem = StartMyStem(myStemPath!);
        }
        else if (TryFindArchive(out var archivePath))
        {
            myStemPath = Extract(archivePath!);
            myStem = StartMyStem(myStemPath);
        }
        else
        {
            archivePath = DownloadMyStem().Result;
            myStemPath = Extract(archivePath);
            myStem = StartMyStem(myStemPath);
        }
    }

    protected string GetWordType(string word)
    {
        myStem.StandardInput.WriteLine(word);
        myStem.StandardInput.Flush();
        
        return wordTypeRegex.Match(ReadMyStemOutput(myStem.StandardOutput)).Groups[0].Value;
    }

    private string ReadMyStemOutput(StreamReader myStemStandardOutput)
    {
        var builder = new StringBuilder();
        while (myStemStandardOutput.Peek() > 0)
        {
            builder.Append((char)myStemStandardOutput.Read());
        }

        return builder.ToString();
    }

    private Process StartMyStem(string filepath)
    {
        return Process.Start(new ProcessStartInfo
        {
            FileName = filepath,
            Arguments = "-i",
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardInputEncoding = Encoding.UTF8,
            StandardOutputEncoding = Encoding.UTF8
        })!;
    }

    private static string Extract(string archivePath)
    {
        var destination = Path.Combine(WorkingDirectory, "mystem");
        ZipFile.ExtractToDirectory(archivePath, destination);

        File.Delete(archivePath);
        return Path.Combine(destination, "mystem.exe");
    }

    private async Task<string> DownloadMyStem()
    {
        var myStemPath = Path.Combine(WorkingDirectory, "mystem.zip");
        using var client = new HttpClient();
        var response = client.GetAsync(MyStemUrl);

        await using var fs = new FileStream(myStemPath, FileMode.CreateNew);
        await response.Result.Content.CopyToAsync(fs);

        return myStemPath;
    }

    private static bool TryFindArchive(out string? filename)
    {
        filename = Directory
            .GetFiles(Environment.CurrentDirectory, "mystem*.zip", SearchOption.AllDirectories)
            .FirstOrDefault();

        return filename != null;
    }

    private static bool TryFindMyStem(out string? filename)
    {
        filename = Directory
            .GetFiles(Environment.CurrentDirectory, "mystem.exe", SearchOption.AllDirectories)
            .FirstOrDefault();

        return filename != null;
    }
}