namespace TagsCloudContainer;

public class WordsHandlerWithFilter : DefaultWordsHandler, IWordsHandler
{
    public Dictionary<string, int> WordDistribution
    {
        get
        {
            if(wordDistribution==null)ProcessSequence();
            return wordDistribution;
        }
        private set => wordDistribution = value;
    }
    private Dictionary<string, int> wordDistribution;
    private readonly IEnumerable<string> wordsToExclude;

    public WordsHandlerWithFilter(IEnumerable<string> wordSequence, IEnumerable<string> wordsToExclude)
        : base(wordSequence)
    {
        this.wordsToExclude = wordsToExclude;
    }

    protected override void ProcessSequence()
    {
        base.ProcessSequence();
        WordDistribution = new Dictionary<string, int>(
            from pair in base.WordDistribution
            where !wordsToExclude.Contains(pair.Key)
            select pair);
    }
}