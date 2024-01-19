using System.Text.RegularExpressions;

namespace TagsCloudVisualization.TextReaders;

public class WordProvider : IWordProvider
{
    private string path;
    
    public WordProvider(string path)
    {
        this.path = path;
    }
    
    public IEnumerable<string> GetWords()
    {
        using var reader = new StreamReader(path);
        var text = reader.ReadToEnd();
        var wordPattern = new Regex(@"\b[a-zA-Z]{2,}\b");
        return wordPattern.Matches(text).Select(x => x.Value);
    }
}