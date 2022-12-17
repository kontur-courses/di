namespace TagCloudContainer;

public class TagCloudProvider : ITagCloudProvider
{
    private readonly ITagConfig _tagConfig;
    private readonly IMainFormConfig _mainFormConfig;
    private readonly IEnumerable<Word> _words;

    public TagCloudProvider(ITagConfig tagConfig, IWordsReader wordsReader, IMainFormConfig mainFormConfig)
    {
        _tagConfig = tagConfig;
        _mainFormConfig = mainFormConfig;
        
        IsValidArguments();
        var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        var wordsFilePath = Path.Combine(projectPath, _mainFormConfig.FileName);
        _words = wordsReader.GetWordsFromFile(wordsFilePath);
    }

    public IEnumerable<Word> GetPreparedWords()
    {
        var sortedWords = _mainFormConfig.Random ? WordsShuffler.ShuffleWords(_words.ToList()) : _words;
        return sortedWords.Select(w => _tagConfig.ConfigureWordTag(w));
    }

    private void IsValidArguments()
    {
        var center = _mainFormConfig.Center;
        var standartSize = _mainFormConfig.StandartSize;
        
        if (standartSize.IsEmpty || standartSize == null)
            throw new ArgumentException("Standart size can not be empty or null");
    }
}