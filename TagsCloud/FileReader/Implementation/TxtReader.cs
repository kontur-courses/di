using TagsCloud.FileConverter;

namespace TagsCloud.FileReader.Implementation;

public class TxtReader : IFileReader
{
    public string[] Read(string path) => File.ReadAllLines(path);
}