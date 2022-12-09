using DocumentFormat.OpenXml.Packaging;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreatorExtensions.WordsFileReaders;

public class DocxFileReader : IFileReader
{
    private readonly IWordsPathSettingsProvider _pathSettingsProvider;

    public DocxFileReader(IWordsPathSettingsProvider pathSettingsProvider)
    {
        _pathSettingsProvider = pathSettingsProvider;
    }

    public string SupportedExtension => ".docx";

    public string ReadFile()
    {
        var dir = _pathSettingsProvider.GetWordsPathSettings().WordsPath;
        using var wordDoc = WordprocessingDocument.Open(dir, false);
        var body = wordDoc.MainDocumentPart!.Document.Body!;
        var lines = body.ChildElements.Select(line => line.InnerText);
        return string.Join("\r\n", lines);
    }
}