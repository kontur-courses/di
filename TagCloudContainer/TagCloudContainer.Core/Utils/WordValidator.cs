using System.Text;
using TagCloudContainer.Core.Interfaces;

namespace TagCloudContainer.Core.Utils;

public class WordValidator : IWordValidator 
{
    private StringBuilder _word;
    private Dictionary<string, int> _boringWords;

    private readonly ITagCloudContainerConfig _tagCloudContainerConfig;

    public string Result
    {
        get => _word.ToString();
    }

    public WordValidator(ITagCloudContainerConfig tagCloudContainerConfig)
    {
        _tagCloudContainerConfig = 
            tagCloudContainerConfig ?? throw new ArgumentNullException("Tag cloud config can't be null");
    }
    
    public IEnumerable<string> Validate(IEnumerable<string> lines)
    {
        var filePath = _tagCloudContainerConfig.ExcludeWordsFilePath;
        _boringWords = File 
            .ReadLines(filePath)
            .Distinct()
            .ToDictionary(w => w, w => 0);
        
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
        _word = new StringBuilder(word.ToLower());
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
