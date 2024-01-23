using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;

namespace TagsCloud.FileReaders;

[SupportedExtension("txt")]
public class TxtFileReader : IFileReader
{
    public IEnumerable<string> ReadContent(string filename)
    {
        return File.ReadLines(filename);
    }
}