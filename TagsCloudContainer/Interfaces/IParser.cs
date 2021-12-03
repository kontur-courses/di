namespace TagsCloudContainer.Interfaces;

public interface IParser
{
    IEnumerable<string> Parse(string path);
}
