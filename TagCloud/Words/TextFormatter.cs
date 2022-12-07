using System.Text.RegularExpressions;

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
        var words = new Dictionary<string, Word>();
        foreach (var word in allWords)
        {
            if (words.ContainsKey(word) == false)
                words[word] = new Word(word);
            words[word].Amount++;
        }

        SetFrequencyToWords(words.Values, allWords.Length);
        return words.Values.ToList();
    }

    private void SetFrequencyToWords(IEnumerable<Word> words, int totalAmountOfWords)
    {
        foreach (var word in words)
            word.Frequency = (float)word.Amount / totalAmountOfWords;
    }

    private void Strip(IEnumerable<Word> words)
    {
        foreach (var word in words) word.Value = word.Value.Trim();
    }
}