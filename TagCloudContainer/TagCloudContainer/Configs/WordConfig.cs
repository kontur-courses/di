using System.Text;

namespace TagCloudContainer;

public class WordConfig : IWordConfig
{
    private StringBuilder _word;
    private Dictionary<string, int> _boringWords;

    public string Result
    {
        get => _word.ToString();
    }
    
    public IEnumerable<string> Validate(IEnumerable<string> lines)
    {
        if (lines == null)
            throw new ArgumentException("Argument is null");

        return lines
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(w => For(w)
                .RemovePunctuationMarks()
                .RemoveIfItBoring()
                .Result)
            .Where(line => !string.IsNullOrWhiteSpace(line));
    }

    public List<Word> ShuffleWords(List<Word> words)
    {
        if (words == null)
            throw new ArgumentException("Argument is null");

        var random = new Random();
         
        for (int i = words.Count() - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            (words[i], words[j]) = (words[j], words[i]);
        }

        return words;
    }

    private WordConfig For(string word)
    {
        if (string.IsNullOrEmpty(word))
            throw new ArgumentException("Word can not be null or empty");
        
        var filePath = Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "boring_words.txt");

        if (!File.Exists(filePath))
            throw new ApplicationException("File does not exists");
        
        _word = new StringBuilder(word.ToLower());
        _boringWords = File 
            .ReadLines(filePath)
            .Distinct()
            .ToDictionary(w => w, w => 0);
        
        return this;
    }

    private WordConfig RemoveIfItBoring()
    {
        if (IsBoring())
            _word.Clear();
        
        return this;
    }

    private WordConfig RemovePunctuationMarks()
    {
        for (var i = 0; i < _word.Length; i++)
            if (char.IsPunctuation(_word[i]))
            {
                _word.Remove(i, 1);
                i--;
            }

        return this;
    }
    
    private bool IsBoring()
    {
        return _boringWords.ContainsKey(_word.ToString());
    }
}
