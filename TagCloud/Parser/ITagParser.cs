namespace TagCloud.Parser;

public interface ITagParser
{
    public TagMap Parse(string filepath);
}