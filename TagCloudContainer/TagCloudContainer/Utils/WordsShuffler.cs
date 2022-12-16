namespace TagCloudContainer;

public static class WordsShuffler
{
    public static List<Word> ShuffleWords(List<Word> words)
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
}