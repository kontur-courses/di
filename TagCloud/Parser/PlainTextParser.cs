using TagCloud.Parser.ParsingConfig;

namespace TagCloud.Parser;

public class PlainTextParser : ITagParser
{
    private IParsingConfig parsingConfig;

    public PlainTextParser(IParsingConfig parsingConfig)
    {
        this.parsingConfig = parsingConfig;
    }

    public TagMap Parse(string filepath)
    {
        var tagMap = new TagMap();
        
        foreach (var line in File.ReadLines(filepath))
        {
            var word = line.ToLower();
            if (!parsingConfig.IsWordExcluded(word))
                tagMap.AddWord(word);
        }

        return tagMap;
    }
}