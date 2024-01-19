namespace TagCloud.TextHandlers;

public class FileTextHandler : ITextHandler
{
    private readonly Stream stream;

    public FileTextHandler(Stream stream)
    {
        this.stream = stream;
    }
    
    public IEnumerable<(string word, int count)> Handle()
    {
        Dictionary<string, int> counts = new();
        using var sr = new StreamReader(stream);
        while(!sr.EndOfStream){
            var t = sr.ReadLine();
            counts.TryAdd(t, 0);
            counts[t]++;
        }
        return counts.Select(pair =>  (pair.Key, pair.Value));
    }
}