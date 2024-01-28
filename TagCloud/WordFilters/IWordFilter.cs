namespace TagCloud.Excluders;

public interface IWordFilter
{
    Dictionary<string, int> ExcludeWords(Dictionary<string, int> counts);
}