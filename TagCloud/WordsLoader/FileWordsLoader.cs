using TagCloud.Abstractions;

namespace TagCloud;

public class FileWordsLoader : IWordsLoader
{
    private readonly string filename;

    public FileWordsLoader(string filename)
    {
        if (!File.Exists(filename))
            throw new ArgumentException($"File {filename} don't exists");
        
        this.filename = filename;
    }
    
    public IEnumerable<string> Load()
    {
        return File.ReadAllLines(filename);
    }
}