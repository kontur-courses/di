namespace TagCloudContainer.Infrastructure.WordWeigher;

public interface IWordWeigher
{
    ICollection<WeightedWord> GetWeightedWords(IEnumerable<string> lines);
}