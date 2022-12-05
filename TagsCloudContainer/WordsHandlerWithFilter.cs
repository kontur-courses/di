namespace TagsCloudContainer;

public class WordsHandlerWithFilter : IWordsHandler
{
    public Dictionary<string, int> WordDistribution { get; }

    public WordsHandlerWithFilter(IEnumerable<string> wordSequence, IEnumerable<string> wordsToExclude)
    {
        DefaultWordsHandler d = new DefaultWordsHandler(wordSequence);
        WordDistribution = new Dictionary<string, int>(
            from pair in d.WordDistribution
            where !wordsToExclude.Contains(pair.Key)
            select pair);

    }
}