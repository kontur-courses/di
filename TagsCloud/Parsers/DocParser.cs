using Spire.Doc;

namespace TagsCloud;

public class DocParser : IParser
{
    public string FileType => "doc";

    public IEnumerable<string?> GetWordList(string filePath)
    {
        var document = new Document(filePath, FileFormat.Doc);
        return ParserHelper.GetTextParagraph(document);
    }
}