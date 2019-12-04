namespace TagCloud
{
    public interface IFilter
    {
        FilterSettings FilterSettings { get; }
        string[] FilterWords(string[] words);
    }
}
