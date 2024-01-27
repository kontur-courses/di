using System.Text.RegularExpressions;
using TagsCloudVisualization.FileReaders;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.TextHandlers;

public class TextHandler : ITextHandler
{
    private readonly IEnumerable<string> words;
    private readonly HashSet<string> boringWords;

    public TextHandler(IFileReaderFactory factory, TextHandlerSettings settings)
    {
        var wordsReader = factory.Create(settings.PathToText);
        var boringWordsReader = factory.Create(settings.PathToBoringWords);
        words = GetWords(wordsReader.ReadText());
        boringWords = GetWords(boringWordsReader.ReadText().ToLower()).ToHashSet();
    }

    public TextHandler(string text, string boringWords)
    {
        words = GetWords(text.ToLower());
        this.boringWords = GetWords(boringWords.ToLower()).ToHashSet();
    }

    public Dictionary<string, int> HandleText()
    {
        var result = new Dictionary<string, int>();

        foreach (var word in words)
        {
            var lowerWord = word.ToLower();
            if (result.ContainsKey(lowerWord))
                result[lowerWord]++;
            else if (!boringWords.Contains(lowerWord))
                result[lowerWord] = 1;
        }

        return result;
    }

    public IEnumerable<string> GetWords(string boringWords)
    {
        var pattern = new Regex(@"\b[\p{L}]+\b", RegexOptions.Compiled);

        foreach (var word in pattern.Matches(boringWords).ToHashSet())
            yield return word.Value;
    }
}
