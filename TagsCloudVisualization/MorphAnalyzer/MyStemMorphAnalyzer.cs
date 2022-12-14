using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace TagsCloudVisualization.MorphAnalyzer;

public class MyStemMorphAnalyzer : IMorphAnalyzer
{
    private readonly string _workingDirectory;

    private readonly string[] _knownGrammems =
    {
        "A", "ADV", "ADVPRO", "ANUM", "APRO", "COM", "CONJ", "INTJ", "NUM", "PART", "PR", "S", "SPRO", "V"
    };

    public MyStemMorphAnalyzer(string workingDirectory)
    {
        _workingDirectory = workingDirectory;

        if (!File.Exists(Path.Combine(_workingDirectory, "mystem.exe")))
            throw new FileNotFoundException($"File mystem.exe not found");
    }

    public Dictionary<string, WordMorphInfo> GetWordsMorphInfo(IEnumerable<string> words)
    {
        const string tempFileName = "mystemTemp.txt";
        var pathToTempFileName = Path.Combine(_workingDirectory, tempFileName);

        File.WriteAllLines(pathToTempFileName, words);

        var proc = new ProcessStartInfo
        {
            UseShellExecute = false,
            WorkingDirectory = Path.Combine(_workingDirectory),
            FileName = @"C:\Windows\System32\cmd.exe",
            Arguments = $"/C mystem.exe -nig --format json {tempFileName}",
            RedirectStandardOutput = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            StandardOutputEncoding = Encoding.UTF8,
        };
        var process = Process.Start(proc);


        var wordsFromMorphAnalyzer = process?.StandardOutput
            .ReadToEnd()
            .Split(Environment.NewLine)
            .ToList()!;

        File.Delete(pathToTempFileName);

        return ParseWordMorphInfoFromOutput(wordsFromMorphAnalyzer);
    }

    private static Dictionary<string, WordMorphInfo> ParseWordMorphInfoFromOutput(List<string> wordsFromMorphAnalyzer)
    {
        var wordsSpeechInfo = new Dictionary<string, WordMorphInfo>();
        foreach (var line in wordsFromMorphAnalyzer)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            var wordInfo = JsonSerializer.Deserialize<MyStemWordInfo>(line);
            if (wordInfo == null)
                continue;

            var wordMorphInfo = new WordMorphInfo();
            foreach (var wordForm in wordInfo.Analysis)
            {
                wordMorphInfo.PartsOfSpeech.Add(wordForm.Gr.Split(',', '=')[0]);
            }

            wordsSpeechInfo[wordInfo.Text] = wordMorphInfo;
        }

        return wordsSpeechInfo;
    }
}