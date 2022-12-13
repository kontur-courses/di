using TagCloud.Abstractions;

namespace TagCloud;

public class FileWordsLoader : IWordsLoader
{
    private readonly string filepath;

    public FileWordsLoader(string filepath)
    {
        if (!File.Exists(filepath))
            throw new FileNotFoundException($"Could not find file '{Path.GetFullPath(filepath)}'.");
        
        this.filepath = filepath;
    }
    
    public IEnumerable<string> Load()
    {
        return File.ReadAllLines(filepath);
    }
}