using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagsCloudCore.WordProcessing.WordInput;

public class DocxFileWordParser : IWordProvider
{
    private readonly string _filePath;
    
    public DocxFileWordParser(string filePath)
    {
        _filePath = filePath;
    }
    
    public string[] Words => Parse();

    private string[] Parse()
    {
        using var wordDocument = WordprocessingDocument.Open(_filePath, false);
        
        var body = wordDocument.MainDocumentPart?.Document.Body;

        if (body is null)
            throw new IOException(
                $"Failed to read from file {_filePath} Most likely the file path is incorrect or the file is corrupted.");

        return body.Elements<Paragraph>()
            .Select(paragraph => paragraph.InnerText)
            .ToArray();
    }
}