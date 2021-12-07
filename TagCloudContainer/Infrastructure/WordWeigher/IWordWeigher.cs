namespace TagCloudContainer.Infrastructure.WordWeigher;

public interface IWordWeigher
{
    Dictionary<string, int> GetWeightedWords(IEnumerable<string> lines);
}