using System.ComponentModel;
using TagCloudContainer.Core.Models;

namespace TagCloudContainer.Core.Utils;

public static class WordsArranger
{
    private static Dictionary<bool, Func<List<Word>, Result<List<Word>>>> _wordsWrappers = new Dictionary<bool, Func<List<Word>, Result<List<Word>>>>
    {
        { true, ShuffleWords },
        { false, WrapWords },
    };

    public static Result<List<Word>> ArrangeWords(List<Word> words, bool random)
    {
        if (words == null)
            return Result.Fail<List<Word>>("Words collection is null");
        return _wordsWrappers[random](words);
    }

    private static Result<List<Word>> ShuffleWords(List<Word> words)
    {
        var random = new Random();
         
        for (int i = words.Count() - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            (words[i], words[j]) = (words[j], words[i]);
        }

        return Result.Ok(words);
    }

    private static Result<List<Word>> WrapWords(List<Word> words) => Result.Ok(words);
}