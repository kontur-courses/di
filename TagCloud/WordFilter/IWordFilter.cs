namespace TagCloud
{
    public interface IWordFilter
    {
        bool IsPermitted(string word);
    }
}
