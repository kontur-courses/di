namespace TagCloudContainer.Infrastructure.WordWeigher;

public interface IWordWeigher
{
    IEnumerable<WeightedWord> GetWeightedWords(IEnumerable<string> lines);
}