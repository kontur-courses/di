namespace TagsCloudContainer.WordsInterfaces;

public interface IWordsAnalyzer
{
    public List<string> Analyze(List<string> words, HashSet<string> boringWords, HashSet<string> spPartToIgnore);
}