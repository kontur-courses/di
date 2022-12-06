using TagCloudApp.Abstractions;

namespace TagCloudApp.Implementations;

public class TxtWordsLoader : IWordsLoader
{
    private readonly WordsFilePathProvider _pathProvider;

    public TxtWordsLoader(WordsFilePathProvider pathProvider)
    {
        _pathProvider = pathProvider;
    }

    public IEnumerable<string> LoadWords()
    {
        var dir = _pathProvider.Path;
        return File.ReadLines(dir);
    }
}