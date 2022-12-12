namespace TagsCloudContainer;

public class DefaultWordsHandler : IWordsHandler
{
    private readonly IEnumerable<string> WordSequence;

    private Dictionary<string, int> wordDistribution;

    public DefaultWordsHandler(IWordSequenceProvider wordSequenceProvider)
    {
        WordSequence = wordSequenceProvider.WordSequence;
    }

    public Dictionary<string, int> WordDistribution
    {
        get
        {
            if (wordDistribution == null) ProcessSequence();
            return wordDistribution;
        }
        protected set => wordDistribution = value;
    }

    protected virtual void ProcessSequence()
    {
        wordDistribution = new Dictionary<string, int>();

        foreach (var word in WordSequence)
        {
            var w = word.ToLower();
            if (wordDistribution.ContainsKey(w)) wordDistribution[w] += 1;
            else wordDistribution.Add(w, 1);
        }
    }
}