namespace TagCloudContainer;

public class TagCloudProvider : ITagCloudProvider
{
    private ITagConfig _tagConfig;
    private IEnumerable<Word> _words;

    public TagCloudProvider(ITagConfig tagConfig, IWordsReader wordsReader)
    {
        _tagConfig = tagConfig;
        IsValidArguments();
        var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        var wordsFilePath = Path.Combine(projectPath, MainFormConfig.FileName);
        _words = wordsReader.GetWordsFromFile(wordsFilePath);
    }

    public IEnumerable<Word> GetPreparedWords()
    {
        var sortedWords = MainFormConfig.Random ? WordsShuffler.ShuffleWords(_words.ToList()) : _words;
        return sortedWords.Select(w => _tagConfig.ConfigureWordTag(w));
    }

    private void IsValidArguments()
    {
        var center = MainFormConfig.Center;
        var standartSize = MainFormConfig.StandartSize;
        
        if (center.IsEmpty || center == null)
            throw new ArgumentException("Center point can not be empty or null");
        if (standartSize.IsEmpty || standartSize == null)
            throw new ArgumentException("Standart size can not be empty or null");
    }
}