namespace TagCloudContainer;

public class WordsReader : IWordsReader
{
    private readonly Dictionary<string, Word> _words = new Dictionary<string, Word>();
    private IWordConfig _wordConfig;

    public WordsReader(IWordConfig wordConfig)
    {
        if (string.IsNullOrEmpty(MainFormConfig.FileName))
            throw new ArgumentException("File name can not be null or empty");
        
        _wordConfig = wordConfig;
    }
    
    public IEnumerable<Word> GetWordsFromFile(string filePath)
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
        lines = MainFormConfig.NeedValidate ? _wordConfig.Validate(lines) : lines;

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
        
        var word = new Word() { Value = wordValue, Weight = 1 };
        _words.Add(wordValue, word);
    }
}