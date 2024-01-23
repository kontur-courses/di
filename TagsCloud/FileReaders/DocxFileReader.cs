using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;

namespace TagsCloud.FileReaders;

// TODO: Implement this reader in future
[SupportedExtension("docx")]
public class DocxFileReader : IFileReader
{
    public IEnumerable<string> ReadContent(string filename)
    {
        throw new NotImplementedException();
    }
}