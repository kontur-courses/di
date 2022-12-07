using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;

namespace TagCloudCreatorExtensions.WordsFileReaders;

public class TxtWordsFileReader : IWordsFileReader
{
    private readonly IWordsPathSettings _pathSettings;

    public TxtWordsFileReader(IWordsPathSettings pathSettings)
    {
        _pathSettings = pathSettings;
    }

    public string SupportedExtension => ".txt";

    public IEnumerable<string> GetWords()
    {
        var dir = _pathSettings.WordsPath;
        return File.ReadLines(dir);
    }
}