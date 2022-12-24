using TagCloudContainer.Core;
using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Core.Models;

namespace TagCloudContainer;

public class WordsReader : IWordsReader
{
    private readonly Dictionary<string, Word> _words = new Dictionary<string, Word>();
    private readonly IWordValidator _wordValidator;
    private readonly ITagCloudContainerConfig _tagCloudContainerConfig;

    public WordsReader(IWordValidator wordValidator, ITagCloudContainerConfig tagCloudContainerConfig)
    {
        Validate(wordValidator, tagCloudContainerConfig);
        
        _tagCloudContainerConfig = tagCloudContainerConfig;
        _wordValidator = wordValidator;
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
        lines = _tagCloudContainerConfig.NeedValidate ? _wordValidator.Validate(lines) : lines;

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

    private void Validate(IWordValidator wordValidator, ITagCloudContainerConfig tagCloudContainerConfig)
    {
        if (wordValidator == null)
            throw new ArgumentException("Word validator can't be null");
        if (tagCloudContainerConfig == null)
            throw new ArgumentException("Tag cloud config can't be null");
    }
}