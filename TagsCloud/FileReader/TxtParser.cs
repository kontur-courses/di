using Spire.Doc;

namespace TagsCloud;

public class TxtParser : IParser
{
    public string FileType => "txt";

    public IEnumerable<string?> GetWordList(string filePath)
    {
        var document = new Document(filePath, FileFormat.Txt);
        return ParserHelper.GetTextParagraph(document);
    }
}