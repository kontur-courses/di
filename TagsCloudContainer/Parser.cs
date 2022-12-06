namespace TagsCloudContainer;

public class Parser
{
    private readonly string? _path;
    private readonly HashSet<string> _boringWords = new()
    {
        "кроме", "между", "перед", "через"
    };

    public Parser(string? path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"There is no file with path: {path}!");
        _path = path;
    }
    
    public (Dictionary<string, int>, int) GetInputWordsFrequency()
    {
        var freqWords = new Dictionary<string, int>();
        var wordsCount = 0;
        
        foreach (var word in GetNextWord())
        {
            var curWord = word.ToLower();
            if (IsBoring(curWord))
                continue;
            
            wordsCount++;
            
            if (!freqWords.ContainsKey(curWord))
                freqWords[curWord] = 0;
            freqWords[curWord]++;
        }

        return (freqWords, wordsCount);
    }

    private IEnumerable<string> GetNextWord()
    {
        var f = new StreamReader(_path);
        while (!f.EndOfStream)
            yield return f.ReadLine();
        
        f.Close();
    }

    private bool IsBoring(string word)
    {
        return word.Length <= 3 || _boringWords.Contains(word);
    }
}