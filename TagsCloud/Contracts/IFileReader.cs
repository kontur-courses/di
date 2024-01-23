namespace TagsCloud.Contracts;

public interface IFileReader
{
    public IEnumerable<string> ReadContent(string filename);
}