using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TagCloud.Parser.ParsingConfig;

namespace TagCloud.Parser;

public class WordDocumentParser : ITagParser
{
    private IParsingConfig parsingConfig;

    public WordDocumentParser(IParsingConfig parsingConfig)
    {
        this.parsingConfig = parsingConfig;
    }
    
    public TagMap Parse(string filepath)
    {
        using var document = WordprocessingDocument.Open(filepath, false);
        if (document.MainDocumentPart?.RootElement == null)
            throw new FormatException($"Could not parse file {filepath}: document lacks the main part.");
        
        var paragraphs = document.MainDocumentPart.RootElement.Descendants<Paragraph>();
        var tagMap = new TagMap();
        foreach (var paragraph in paragraphs)
        {
            var word = paragraph.InnerText.ToLower();
            if (!parsingConfig.IsWordExcluded(word))
                tagMap.AddWord(word);
        }

        return tagMap;
    }
}