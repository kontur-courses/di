namespace TagsCloud.Contracts;

public interface IFileReader
{
    string SupportedExtension { get; }
    IEnumerable<string> ReadContent(string filename, IPostFormatter postFormatter = null);
}