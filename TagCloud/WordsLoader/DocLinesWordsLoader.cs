using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TagCloud.Abstractions;

namespace TagCloud;

public class DocLinesWordsLoader : IWordsLoader
{
    private readonly string filepath;

    public DocLinesWordsLoader(string filepath)
    {
        if (!File.Exists(filepath))
            throw new FileNotFoundException($"Could not find file '{Path.GetFullPath(filepath)}'.");

        this.filepath = filepath;
    }

    public IEnumerable<string> Load()
    {
        using var document = WordprocessingDocument.Open(filepath, false);
        var paragraphs = document.MainDocumentPart.Document.Body.Descendants<Paragraph>();

        foreach (var paragraph in paragraphs)
            yield return paragraph.InnerText;
    }
}