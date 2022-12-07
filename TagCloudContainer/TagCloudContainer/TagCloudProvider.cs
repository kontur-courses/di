namespace TagCloudContainer;

public class TagCloudProvider
{
    private WordsReader _wordsReader = new WordsReader();
    private TagConfig _tagConfig;
    private IEnumerable<Word> _words;

    public TagCloudProvider(string fileName, Point center, Size standartSize)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("File name can not be null or empty");
        if (center.IsEmpty)
            throw new ArgumentException("Center point can not be empty");
        if (standartSize.IsEmpty)
            throw new ArgumentException("Standart size can not be empty");

        _tagConfig = new TagConfig(center, standartSize);
        _words = _wordsReader.GetWordsFromFile(fileName, true);
    }

    public IEnumerable<Word> GetPreparedWords()
    {
        return _words.Select(w => _tagConfig.ConfigureWordTag(w));
    }
}