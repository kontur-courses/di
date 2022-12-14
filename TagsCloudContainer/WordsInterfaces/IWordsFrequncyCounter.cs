namespace TagsCloudContainer.WordsInterfaces;

public interface IWordsFrequencyCounter
{
    public Dictionary<string, double> Count(List<string> words);
}