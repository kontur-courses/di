namespace TagsCloudContainer;

public class WordsHandlerWithFilter : DefaultWordsHandler, IWordsHandler
{
    private readonly IEnumerable<string> wordsToExclude;

    public WordsHandlerWithFilter(IWordSequenceProvider wordSequenceProvider, IWordFilterProvider wordsFilterProvider)
        : base(wordSequenceProvider)
    {
        if (!wordsFilterProvider.WordFilter.Successful)
            WordDistribution = new Result<Dictionary<string, int>>(wordsFilterProvider.WordFilter.Exception);
        else
            wordsToExclude =
                from word in wordsFilterProvider.WordFilter.Value
                select word.ToLower();
    }

    protected override Dictionary<string, int> ProcessSequence()
    {
        var baseProcess = base.ProcessSequence();
        var distribution = new Dictionary<string, int>(
            new Dictionary<string, int>(
                from pair in baseProcess
                where !wordsToExclude.Contains(pair.Key)
                select pair));
        return distribution;
    }
}