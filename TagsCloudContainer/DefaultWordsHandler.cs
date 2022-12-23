namespace TagsCloudContainer;

public class DefaultWordsHandler : IWordsHandler
{
    private readonly IEnumerable<string> WordSequence;
    private Result<Dictionary<string, int>> wordDistribution;

    public DefaultWordsHandler(IWordSequenceProvider wordSequenceProvider)
    {
        var wordSequenceResult = wordSequenceProvider.WordSequence;
        if (!wordSequenceResult.Successful)
            wordDistribution = new Result<Dictionary<string, int>>(wordSequenceResult.Exception);
        else WordSequence = wordSequenceResult.Value;
    }

    public Result<Dictionary<string, int>> WordDistribution
    {
        get
        {
            if (wordDistribution == null)
                wordDistribution = Result.GetResult(ProcessSequence);
            return wordDistribution;
        }
        protected set => wordDistribution = value;
    }

    protected virtual Dictionary<string, int> ProcessSequence()
    {
        var wordD = new Dictionary<string, int>();

        foreach (var word in WordSequence)
        {
            var w = word.ToLower();
            if (wordD.ContainsKey(w)) wordD[w] += 1;
            else wordD.Add(w, 1);
        }

        return new Dictionary<string, int>(wordD);
    }
}