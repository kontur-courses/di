using System.Text;

namespace TagCloudContainer;

public class WordConfig : IWordConfig
{
    private StringBuilder _word;
    private Dictionary<string, int> _boringWords;
    
    private readonly IMainFormConfig _mainFormConfig;

    public string Result
    {
        get => _word.ToString();
    }

    public WordConfig(IMainFormConfig mainFormConfig)
    {
        _mainFormConfig = mainFormConfig;
    }
    
    public IEnumerable<string> Validate(IEnumerable<string> lines)
    {
        if (lines == null)
            throw new ArgumentException("Argument is null");

        return lines
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(w => ForWord(w)
                .RemovePunctuationMarks()
                .RemoveIfItBoring()
                .Result)
            .Where(line => !string.IsNullOrWhiteSpace(line));
    }

    private WordConfig ForWord(string word)
    {
        if (string.IsNullOrEmpty(word))
            throw new ArgumentException("Word can not be null or empty");
        
        var filePath = Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, _mainFormConfig.ExcludeWordsFileName);

        if (!File.Exists(filePath))
            throw new ApplicationException("File does not exists");
        
        _word = new StringBuilder(word.ToLower());
        _boringWords = File 
            .ReadLines(filePath)
            .Distinct()
            .ToDictionary(w => w, w => 0);
        
        return this;
    }

    private WordConfig RemoveIfItBoring()
    {
        if (IsBoring())
            _word.Clear();
        
        return this;
    }

    private WordConfig RemovePunctuationMarks()
    {
        for (var i = 0; i < _word.Length; i++)
            if (char.IsPunctuation(_word[i]))
            {
                _word.Remove(i, 1);
                i--;
            }

        return this;
    }
    
    private bool IsBoring()
    {
        return _boringWords.ContainsKey(_word.ToString());
    }
}
