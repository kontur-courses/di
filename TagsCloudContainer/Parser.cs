using DeepMorphy;
using NHunspell;

namespace TagsCloudContainer;

public class Parser
{
    private readonly string? _path;
    private readonly HashSet<string> _boringWords = new();

    private readonly HashSet<string> _spPartToIgnore = new()
    {
        "мест", "предл"
    };

    public Parser(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"There is no file with path: {path}!");
        _path = path;
    }
    
    public Parser(string? path, HashSet<string> wordsToIgnore, HashSet<string> spPartToIgnore)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"There is no file with path: {path}!");
        _path = path;
        
        _boringWords.UnionWith(wordsToIgnore);
        _spPartToIgnore.UnionWith(spPartToIgnore);
    }
    
    public void ExceptWords(HashSet<string> words)
    {
        _boringWords.UnionWith(words);
    }

    public void ExceptSpeechPart(string spPart)
    {
        _spPartToIgnore.Add(spPart);
    }
    
    public Dictionary<string, double> GetInputWordsFrequency()
    {
        var freqWords = new Dictionary<string, double>();
        var wordsCount = 0;
        
        foreach (var word in GetNonBoringWord())
        {
            var curWord = word.ToLower();
            
            wordsCount++;
            
            if (!freqWords.ContainsKey(curWord))
                freqWords[curWord] = 0;
            freqWords[curWord]++;
        }

        foreach (var key in freqWords.Keys)
            freqWords[key] = Math.Round(freqWords[key] / wordsCount, 2);

        return freqWords;
    }

    private IEnumerable<string> GetNonBoringWord()
    {
        var morph = new MorphAnalyzer(withLemmatization: true);
        foreach (var word in GetNextWord())
        {
            var wordData = morph.Parse(word).ToArray()[0];
            var spPart = wordData.BestTag.GramsDic["чр"];
            if (!_spPartToIgnore.Contains(spPart) && !_boringWords.Contains(word)) // Если часть речи местоимение или предлог
                yield return wordData.BestTag.Lemma;
        }
    }
    
    private IEnumerable<string> GetNextWord()
    {
        var f = new StreamReader(_path);
        while (!f.EndOfStream)
            yield return f.ReadLine();
        
        f.Close();
    }
}