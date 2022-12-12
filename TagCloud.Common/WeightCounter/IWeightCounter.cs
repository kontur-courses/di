namespace TagCloud.Common.WeightCounter;

public interface IWeightCounter
{
    Dictionary<string, int> CountWeights(IEnumerable<string> lines);
}