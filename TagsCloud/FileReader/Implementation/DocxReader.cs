using TagsCloud.FileConverter;

namespace TagsCloud.FileReader.Implementation;

public class DocxReader : IFileReader
{
    private readonly IFileConverter converter;

    public DocxReader(IFileConverter converter)
    {
        this.converter = converter;
    }

    public string[] Read(string path)
    {
        var newPath = converter.Convert(path);
        return File.ReadAllLines(newPath).Skip(1).ToArray();
    }
}