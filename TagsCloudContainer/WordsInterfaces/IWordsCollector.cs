namespace TagsCloudContainer.WordsInterfaces;

public interface IWordsCollector
{
    public (Dictionary<string, double>, int) Collect(string? path, HashSet<string> boringWords,
        HashSet<string> spPartToIgnore);
}