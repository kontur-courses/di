using Spire.Doc;

namespace TagsCloud;

public class DocxParser : IParser
{
    public string FileType => "docx";

    public IEnumerable<string?> GetWordList(string filePath)
    {
        var document = new Document(filePath, FileFormat.Docx);
        return ParserHelper.GetTextParagraph(document);
    }
}