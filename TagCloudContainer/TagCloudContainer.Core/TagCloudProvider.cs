using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Core.Models;
using TagCloudContainer.Core.Utils;

namespace TagCloudContainer.Core;

public class TagCloudProvider : ITagCloudProvider
{
    private readonly ITagCloudPlacer _tagCloudPlacer;
    private readonly IEnumerable<Word> _words;
    private readonly ITagCloudContainerConfig _tagCloudContainerConfig;

    public TagCloudProvider(
        ITagCloudPlacer tagCloudPlacer, 
        IWordsReader wordsReader, 
        ITagCloudContainerConfig tagCloudContainerConfig)
    {
        Validate(tagCloudPlacer, wordsReader, tagCloudContainerConfig);
        
        _tagCloudPlacer = tagCloudPlacer;
        _tagCloudContainerConfig = tagCloudContainerConfig;
        
        var wordsFilePath = _tagCloudContainerConfig.FilePath;
        _words = wordsReader.GetWordsFromFile(wordsFilePath);
    }

    public Result<List<Word>> GetPreparedWords()
    {
        var result = (new List<Word>()).AsResult();
        var arrangedWords = WordsArranger.ArrangeWords(_words.ToList(), _tagCloudContainerConfig.Random);

        if (!arrangedWords.IsSuccess)
            return Result.Fail<List<Word>>(arrangedWords.Error);

        foreach (var word in arrangedWords.GetValueOrThrow())
        {
            var wordResult = _tagCloudPlacer.PlaceInCloud(word);
            if (!wordResult.IsSuccess)
                return Result.Fail<List<Word>>(wordResult.Error);
            result.Value.Add(wordResult.Value);
        }

        return result;
    }

    public void Validate(
        ITagCloudPlacer tagCloudPlacer, 
        IWordsReader wordsReader, 
        ITagCloudContainerConfig tagCloudContainerConfig)
    {
        if (tagCloudPlacer == null)
            throw new ArgumentException("Tag cloud placer can't be null");
        if (wordsReader == null)
            throw new ArgumentException("Word reader can't be null");
        if (tagCloudContainerConfig == null)
            throw new ArgumentException("Tag cloud config can't be null");
    }
}