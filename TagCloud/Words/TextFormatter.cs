using System.Text.RegularExpressions;
using MoreLinq.Extensions;

namespace TagCloud.Words;

public class TextFormatter
{
    public IList<Word> Format(string text)
    {
        text = RemovePunctuations(text);
        text = ConvertToLowerCase(text);
        var words = GetAllWordsFromText(text);
        words = words.Where(word => string.IsNullOrWhiteSpace(word.Value) == false).ToList();
        Strip(words);
        return words;
    }

    private string RemovePunctuations(string text)
    {
        return Regex.Replace(text, "\\p{P}", string.Empty);
    }


    private string ConvertToLowerCase(string text)
    {
        return text.ToLower();
    }

    private IList<Word> GetAllWordsFromText(string text)
    {
        var allWords = text.Split(Environment.NewLine);
        
        var words = allWords
            .CountBy(word => word)
            .Select(wordsWithAmount => 
                new Word(wordsWithAmount.Key, (float)wordsWithAmount.Value / allWords.Length))
            .ToList();

        return words;
    }

    private void Strip(IEnumerable<Word> words)
    {
        foreach (var word in words) word.Value = word.Value.Trim();
    }
}