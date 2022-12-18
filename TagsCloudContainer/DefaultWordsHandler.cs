namespace TagsCloudContainer;

public class DefaultWordsHandler : IWordsHandler
{
    private readonly IEnumerable<string> WordSequence;

    private Result<Dictionary<string, int>> wordDistribution;

    public DefaultWordsHandler(IWordSequenceProvider wordSequenceProvider)
    {
        var wordSequenceResult = wordSequenceProvider.WordSequence;
        if (wordSequenceProvider.WordSequence.Successful) WordSequence = wordSequenceResult.Value;
        else WordDistribution = new Result<Dictionary<string, int>>(wordSequenceResult.Exception);
    }

    public Result<Dictionary<string, int>> WordDistribution
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
        var wordD = new Dictionary<string, int>();

        foreach (var word in WordSequence)
        {
            var w = word.ToLower();
            if (wordD.ContainsKey(w)) wordD[w] += 1;
            else wordD.Add(w, 1);
        }

        wordDistribution = new Result<Dictionary<string, int>>(wordD);
    }
}