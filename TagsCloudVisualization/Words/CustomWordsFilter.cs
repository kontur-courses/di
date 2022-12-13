namespace TagsCloudVisualization.Words;

public class CustomWordsFilter : IWordsFilter
{
    public Dictionary<string, int> FilterWords(Dictionary<string, int> wordsAndCount)
    {
        var filteredWords = new Dictionary<string, int>();
        foreach (var wordCountPair in wordsAndCount)
        {
            if (wordCountPair.Key.Length < 3)
                continue;

            filteredWords.Add(wordCountPair.Key, wordCountPair.Value);
        }

        return filteredWords;
    }
}