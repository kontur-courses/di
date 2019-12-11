namespace TagCloud
{
    public interface IFilter : ICheckable
    {
        string[] FilterWords(string[] words);
    }
}
