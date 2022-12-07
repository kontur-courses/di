namespace TagCloud.Parser.ParsingConfig;

public interface IParsingConfig
{
    public bool IsWordExcluded(string word);
}