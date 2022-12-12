namespace TagCloud.Common.WeightCounter;

public class SimpleWeightCounter : IWeightCounter
{
    public Dictionary<string, int> CountWeights(IEnumerable<string> lines)
    {
        return lines
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count())
            .OrderByDescending(x => x.Value)
            .ToDictionary(x => x.Key, x => x.Value);
    }
}