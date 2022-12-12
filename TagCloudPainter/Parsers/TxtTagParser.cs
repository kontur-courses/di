using System.Text;
using TagCloudPainter.Common;
using TagCloudPainter.Interfaces;

namespace TagCloudPainter.Parsers;

public class TxtTagParser : ITagParser
{
    private readonly Mystem.Net.Mystem mystem;
    private readonly ParseSettings parseSettings;

    public TxtTagParser(IParseSettingsProvider parseSettingsProvider)
    {
        mystem = new Mystem.Net.Mystem();
        parseSettings = parseSettingsProvider.ParseSettings;
    }

    public Dictionary<string, int> ParseTags(string path)
    {
        var wordsCount = new Dictionary<string, int>();
        var words = ReadText(path);
        foreach (var word in words)
        {
            if (IsBoringWord(word))
                continue;
            var key = GetLemma(word).ToLower();
            if (wordsCount.ContainsKey(key))
                wordsCount[key]++;
            else
                wordsCount[key] = 1;
        }

        return wordsCount;
    }

    private IEnumerable<string> ReadText(string path)
    {
        var words = new List<string>();
        using (var reader = new StreamReader(path, Encoding.UTF8))
        {
            string? line;
            while ((line = reader.ReadLine()) != null) 
                words.Add(line);
        }

        return words;
    }

    private bool IsBoringWord(string word)
    {
        if (parseSettings.IgnoredWords.Count>0 && parseSettings.IgnoredWords.Contains(word))
            return true;
        var analizedWord = mystem.Mystem.Analyze(word).Result[0];
        var morpheme = analizedWord.AnalysisResults[0].Grammeme.Split('=', ',')[0];
        if (morpheme == null)
            return true;

        if (parseSettings.IgnoredMorphemes.Contains(morpheme))
            return true;

        return false;
    }

    private string GetLemma(string word)
    {
        return mystem.Mystem.Lemmatize(word).Result[0];
    }
}