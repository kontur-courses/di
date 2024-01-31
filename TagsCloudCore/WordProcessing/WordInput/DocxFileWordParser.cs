using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TagsCloudCore.Common.Enums;

namespace TagsCloudCore.WordProcessing.WordInput;

public class DocxFileWordParser : IWordProvider
{
    public string[] GetWords(string resourceLocation)
    {
        using var wordDocument = WordprocessingDocument.Open(resourceLocation, false);

        var body = wordDocument.MainDocumentPart?.Document.Body;

        if (body is null)
            throw new IOException(
                $"Failed to read from file {resourceLocation} Most likely the file path is incorrect or the file is corrupted.");

        return body.Elements<Paragraph>()
            .Select(paragraph => paragraph.InnerText)
            .ToArray();
    }

    public WordProviderType Info => WordProviderType.Docx;

    public bool Match(WordProviderType info)
    {
        return info == Info || info == WordProviderType.Doc;
    }
}