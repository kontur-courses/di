namespace TagCloudPainter.FileReader;

public class TxtReader : IFileReader
{
    public IEnumerable<string> ReadFile(string path)
    {
        return File.ReadAllLines(path).Where(x => x != "");
    }
}