using System.Xml.XPath;

namespace TagCloud.FileReader;

public class TxtReader : IFileReader
{
    private List<string> extensions = new() { "txt" };
    
    public IEnumerable<string> ReadLines(string InputPath)
    {
        if (!File.Exists(InputPath))
            throw new ArgumentException("Source file doesn't exist");

        return File.ReadLines(InputPath);
    }

    public IList<string> GetAviableExtensions()
    {
        return extensions;
    }
}