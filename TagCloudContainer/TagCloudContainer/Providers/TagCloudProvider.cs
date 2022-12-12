namespace TagCloudContainer;

public class TagCloudProvider
{
    private WordsReader _wordsReader = new WordsReader();
    private IMainFormConfig _mainFormConfig;
    private ITagConfig _tagConfig;
    private IEnumerable<Word> _words;

    public TagCloudProvider(string fileName, Point center, Size standartSize, IMainFormConfig mainFormConfig)
    {
        IsValidArguments(fileName, center, standartSize);
        _tagConfig = new TagConfig(center, standartSize, mainFormConfig);
        _words = _wordsReader.GetWordsFromFile(fileName, true, mainFormConfig);
        _mainFormConfig = mainFormConfig;
    }

    public IEnumerable<Word> GetPreparedWords()
        => _words.Select(w => _tagConfig.ConfigureWordTag(w, _mainFormConfig));

    private void IsValidArguments(string fileName, Point center, Size standartSize)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("File name can not be null or empty");
        if (center.IsEmpty || center == null)
            throw new ArgumentException("Center point can not be empty or null");
        if (standartSize.IsEmpty || standartSize == null)
            throw new ArgumentException("Standart size can not be empty or null");
    }
}