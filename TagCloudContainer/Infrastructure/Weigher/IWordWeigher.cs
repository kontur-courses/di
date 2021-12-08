namespace TagCloud.Infrastructure.Weigher;

public interface IWordWeigher
{
    Dictionary<string, int> GetWeightedWords(IEnumerable<string> lines);
}