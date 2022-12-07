using TagCloud.Abstractions;

namespace TagCloud;

public class FileWordsLoader : IWordsLoader
{
    private readonly string filename;

    public FileWordsLoader(string filename)
    {
        this.filename = filename;
    }
    
    public IEnumerable<string> Load()
    {
        return File.ReadAllLines(filename);
    }
}