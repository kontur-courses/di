using TagsCloud.Contracts;

namespace TagsCloud.FileReaders;

// TODO: Implement this reader in future
public class CsvFileReader : IFileReader
{
    public string SupportedExtension => "csv";

    public IEnumerable<string> ReadContent(string filename, IPostFormatter postFormatter = null)
    {
        throw new NotImplementedException();
    }
}