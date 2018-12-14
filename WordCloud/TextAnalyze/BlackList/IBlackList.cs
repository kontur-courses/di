namespace WordCloud.TextAnalyze
{
    public interface IBlackList
    {
        bool Countains(string word);
        int Count { get; }
    }
}