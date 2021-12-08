namespace TagCloud.Infrastructure.WordWeigher;

public interface IWordWeigher
{
    Dictionary<string, int> GetWeightedWords(IEnumerable<string> lines);
}