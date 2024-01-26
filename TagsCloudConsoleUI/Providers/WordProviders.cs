using TagsCloudCore.WordProcessing.WordInput;

namespace TagsCloudConsoleUI.Providers;

public static class WordProviders
{
    public static readonly IReadOnlyDictionary<string, Func<string, IWordProvider>> RegisteredProviders =
        new Dictionary<string, Func<string, IWordProvider>>
    {
        {".txt", path=> new TxtFileWordParser(path)},
        {".docx", path => new DocxFileWordParser(path)},
        {".doc", path => new DocxFileWordParser(path)}
    };
}