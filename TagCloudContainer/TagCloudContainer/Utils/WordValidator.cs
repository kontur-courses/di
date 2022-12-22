using System.Text;
using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer;

public class WordValidator : IWordValidator 
{
    private StringBuilder _word;
    private Dictionary<string, int> _boringWords;

    private readonly IWordReaderConfig _wordReaderConfig;

    public string Result
    {
        get => _word.ToString();
    }

    public WordValidator(IWordReaderConfig wordReaderConfig)
    {
        _wordReaderConfig = wordReaderConfig;
    }
    
    public IEnumerable<string> Validate(IEnumerable<string> lines)
    {
        return lines
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(w => ForWord(w)
                .RemovePunctuationMarks()
                .RemoveIfItBoring()
                .Result)
            .Where(line => !string.IsNullOrWhiteSpace(line));
    }

    private WordValidator ForWord(string word)
    {
        var filePath = _wordReaderConfig.ExcludeWordsFilePath;
        
        _word = new StringBuilder(word.ToLower());
        _boringWords = File 
            .ReadLines(filePath)
            .Distinct()
            .ToDictionary(w => w, w => 0);
        
        return this;
    }

    private WordValidator RemoveIfItBoring()
    {
        if (IsBoring())
            _word.Clear();
        
        return this;
    }

    private WordValidator RemovePunctuationMarks()
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
