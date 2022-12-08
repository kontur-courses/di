using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreatorExtensions.WordsFileReaders;

public class TxtFileReader : IFileReader
{
    private readonly IWordsPathSettingsProvider _pathSettingsProvider;

    public TxtFileReader(IWordsPathSettingsProvider pathSettingsProvider)
    {
        _pathSettingsProvider = pathSettingsProvider;
    }

    public string SupportedExtension => ".txt";

    public string ReadFile()
    {
        var dir = _pathSettingsProvider.GetWordsPathSettings().WordsPath;
        return File.ReadAllText(dir);
    }
}