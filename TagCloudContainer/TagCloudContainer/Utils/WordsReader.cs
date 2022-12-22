using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer;

public class WordsReader : IWordsReader
{
    private readonly Dictionary<string, TagCloudContainer.Additions.Models.Word> _words = new Dictionary<string, TagCloudContainer.Additions.Models.Word>();
    private readonly IWordValidator _wordValidator;
    private readonly IWordReaderConfig _wordReaderConfig;

    public WordsReader(IWordValidator wordValidator, IWordReaderConfig wordReaderConfig)
    {
        _wordReaderConfig = wordReaderConfig;
        _wordValidator = wordValidator;
    }
    
    public IEnumerable<Additions.Models.Word> GetWordsFromFile(string filePath)
    {
        Read(filePath);
        var wordsList = _words.Values.ToList();
        return wordsList.OrderByDescending(w => w.Weight);
    }

    private void Read(string filePath)
    {
        var lines = File
            .ReadLines(filePath)
            .Distinct();
        lines = _wordReaderConfig.NeedValidate ? _wordValidator.Validate(lines) : lines;

        foreach (var word in lines)
            AddWord(word);
    }

    private void AddWord(string wordValue)
    {
        if (_words.ContainsKey(wordValue))
        {
            _words[wordValue].Weight++;
            return;
        }
        
        var word = new TagCloudContainer.Additions.Models.Word() { Value = wordValue, Weight = 1 };
        _words.Add(wordValue, word);
    }
}