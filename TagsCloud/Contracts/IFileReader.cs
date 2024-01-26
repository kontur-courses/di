namespace TagsCloud.Contracts;

public interface IFileReader
{
    IEnumerable<string> ReadContent(string filename);
}