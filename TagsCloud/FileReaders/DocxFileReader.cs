using TagsCloud.Contracts;

namespace TagsCloud.FileReaders;

// TODO: Implement this reader in future
public class DocxFileReader : IFileReader
{
    public string SupportedExtension => "docx";

    public IEnumerable<string> ReadContent(string filename, IPostFormatter postFormatter = null)
    {
        throw new NotImplementedException();
    }
}