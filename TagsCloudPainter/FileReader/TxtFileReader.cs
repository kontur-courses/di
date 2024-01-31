namespace TagsCloudPainter.FileReader;

internal class TxtFileReader : IFileReader
{
    public string ReadFile(string path)
    {
        return File.ReadAllText(path).Trim();
    }
}