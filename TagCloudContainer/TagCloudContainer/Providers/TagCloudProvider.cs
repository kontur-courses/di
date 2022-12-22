using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer;

public class TagCloudProvider : ITagCloudProvider
{
    private readonly ITagCloudPlacer _tagCloudPlacer;
    private readonly IEnumerable<Additions.Models.Word> _words;
    private readonly IWordReaderConfig _wordReaderConfig;
    private readonly ITagCloudContainerConfig _tagCloudContainerConfig;

    public TagCloudProvider(
        ITagCloudPlacer tagCloudPlacer, 
        IWordsReader wordsReader, 
        IWordReaderConfig wordReaderConfig,
        ITagCloudContainerConfig tagCloudContainerConfig)
    {
        _tagCloudPlacer = tagCloudPlacer;
        _wordReaderConfig = wordReaderConfig;
        _tagCloudContainerConfig = tagCloudContainerConfig;
        
        var wordsFilePath = _wordReaderConfig.FilePath;
        _words = wordsReader.GetWordsFromFile(wordsFilePath);
    }

    public IEnumerable<Additions.Models.Word> GetPreparedWords()
    {
        var sortedWords = _tagCloudContainerConfig.Random 
            ? WordsShuffler.ShuffleWords(_words.ToList()) : _words;
        return sortedWords.Select(w => _tagCloudPlacer.PlaceInCloud(w));
    }
}