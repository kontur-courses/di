using System.Xml.XPath;

namespace TagCloud.FileReader;

public class TxtReader : IFileReader
{
    private List<string> extensions = new() { "txt" };

    public IEnumerable<string> ReadLines(string inputPath)
    {
        if (!File.Exists(inputPath))
            throw new ArgumentException("Source file doesn't exist");

        return File.ReadLines(inputPath);
    }

    public IList<string> GetAvailableExtensions()
    {
        return extensions;
    }
}