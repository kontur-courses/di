using TagsCloud.Contracts;

namespace TagsCloud.FileReaders;

public class TxtFileReader : IFileReader
{
    public string SupportedExtension => "txt";

    public IEnumerable<string> ReadContent(string filename, IPostFormatter postFormatter = null)
    {
        return File
               .ReadLines(filename)
               .Where(line => !string.IsNullOrEmpty(line))
               .Select(line => postFormatter is null ? line : postFormatter.Format(line));
    }
}