namespace TagCloudContainer.Infrastructure.WordWeigher;

public class WeightedWord
{
    public string Word { get; }
    public int Weight { get; }

    public WeightedWord(string word, int weight)
    {
        Word = word;
        Weight = weight;
    }
}