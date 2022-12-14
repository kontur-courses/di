using TagsCloudContainer.WordsInterfaces;

namespace TagsCloudContainer;

public class WordsFrequencyCounter : IWordsFrequencyCounter
{
    public Dictionary<string, double> Count(List<string> words)
    {
        var freqWords = new Dictionary<string, double>();
        var wordsCount = 0;

        foreach (var word in words)
        {
            var curWord = word.ToLower();

            wordsCount++;

            if (!freqWords.ContainsKey(curWord))
                freqWords[curWord] = 0;
            freqWords[curWord]++;
        }

        foreach (var key in freqWords.Keys)
            freqWords[key] = Math.Round(freqWords[key] / wordsCount, 6);

        return freqWords;
    }
}