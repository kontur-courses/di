namespace TagsCloudContainer;

public class WordsHandlerWithFilter : DefaultWordsHandler, IWordsHandler
{
    private readonly IEnumerable<string> wordsToExclude;

    private Dictionary<string, int> wordDistribution;

    public WordsHandlerWithFilter(IWordSequenceProvider wordSequenceProvider, IWordFilterProvider wordsFilterProvider)
        : base(wordSequenceProvider)
    {
        if (wordsFilterProvider.WordFilter.Successful)
            wordsToExclude =
                from word in wordsFilterProvider.WordFilter.Value
                select word.ToLower();
        else WordDistribution = new Result<Dictionary<string, int>>(wordsFilterProvider.WordFilter.Exception);
    }

    protected override void ProcessSequence()
    {
        base.ProcessSequence();
        WordDistribution = new Result<Dictionary<string, int>>(
            new Dictionary<string, int>(
            from pair in base.WordDistribution.Value
            where !wordsToExclude.Contains(pair.Key)
            select pair));
    }
}