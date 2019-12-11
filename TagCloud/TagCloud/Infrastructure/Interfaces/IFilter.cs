namespace TagCloud
{
    public interface IFilter
    {
        string[] FilterWords(string[] words);
    }
}
