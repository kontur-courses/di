namespace TagCloud
{
    public interface IParser : ICheckable
    {
        string[] ParseWords(string[] words);
    }
}
