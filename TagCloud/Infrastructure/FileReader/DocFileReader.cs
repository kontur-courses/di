using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagCloud.Infrastructure.FileReader;

public class DocFileReader : IFileReader
{
    private static readonly IReadOnlySet<string> SupportedExtensions = new HashSet<string> { ".docx", ".doc" };

    public IEnumerable<string> GetLines(string inputPath)
    {
        using var document = WordprocessingDocument.Open(inputPath, false);
        var parapgraphs = document.MainDocumentPart.RootElement;

        return parapgraphs
            .Descendants<Paragraph>()
            .Select(x => x.InnerText);
    }

    public IReadOnlySet<string> GetSupportedExtensions()
    {
        return SupportedExtensions;
    }
}