using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;

public class BoringWordsFromUser : IBoringWords
{
    private readonly List<string> usersWords;
    
    public BoringWordsFromUser()
    {
        this.usersWords = new List<string>();
    }

    public bool IsBoring(IWord word)
    {
        return usersWords.Any(boringWord => boringWord == word.Value);
    }

    public void AddBoringWord(string word)
    {
        usersWords.Add(word);
    }
}