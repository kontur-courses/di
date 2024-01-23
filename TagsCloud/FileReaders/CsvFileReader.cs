using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;

namespace TagsCloud.FileReaders;

// TODO: Implement this reader in future
[SupportedExtension("csv")]
public class CsvFileReader : IFileReader
{
    public IEnumerable<string> ReadContent(string filename)
    {
        throw new NotImplementedException();
    }
}