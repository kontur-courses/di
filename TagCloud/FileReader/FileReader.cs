namespace TagCloud.FileReader;

public class FileReader : IFileReader
{
    public IEnumerable<string> ReadLines(string InputPath)
    {
        if (!File.Exists(InputPath))
            throw new ArgumentException("Source file doesn't exist");

        return File.ReadLines(InputPath);
    }
}