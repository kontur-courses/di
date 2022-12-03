namespace TagsCloudContainer;

public class DefaultWordsHandler : IWordsHandler
{
    private readonly IEnumerable<string> WordSequence;

    public DefaultWordsHandler(IEnumerable<string> WordSequence)
    {
        this.WordSequence = WordSequence;
        ProcessSequence();
    }

    public Dictionary<string, int> WordDistribution { get; } = new();

    private void ProcessSequence()
    {
        foreach (var word in WordSequence)
        {
            var w = word.ToLower();
            if (WordDistribution.ContainsKey(w)) WordDistribution[w] += 1;
            else WordDistribution.Add(w, 1);
        }
    }
}