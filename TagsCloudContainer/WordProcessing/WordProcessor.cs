namespace TagsCloudContainer.WordProcessing;

public class WordProcessor
{
    private Dictionary<string, Word> _boringWords;
    private readonly Settings _settings;
    private readonly string[] _separators = {"\r", "\n", ", ", ". ", " "};

    public WordProcessor(Settings settings)
    {
        _settings = settings;
    }

    private void LoadBoringWords(string text)
    {
        _boringWords = CreateWordDictionaryBasedOnText(text);
    }

    public List<Word> ProcessWords(string text, string boringText = "")
    {
        LoadBoringWords(boringText);

        var wordsDictionary = CreateWordDictionaryBasedOnText(text);
        var filteredWords = wordsDictionary
            .Select(wordPair => wordPair.Value)
            .Where(word => !_boringWords.ContainsKey(word.Value) && word.Value.Length >= 3)
            .ToList();

        foreach (var word in filteredWords )
            word.GenerateFontSize(_settings, filteredWords .Count);

        return filteredWords;
    }

    private Dictionary<string, Word> CreateWordDictionaryBasedOnText(string text)
    {
        var wordDictionary = new Dictionary<string, Word>();
        foreach (var word in text.Split(_separators, StringSplitOptions.RemoveEmptyEntries))
        {
            var wordKey = word.ToLower();
            if (!wordDictionary.ContainsKey(wordKey))
                wordDictionary.Add(wordKey, new Word(wordKey));
            else
                wordDictionary[wordKey].Count++;
        }

        return wordDictionary;
    }
}