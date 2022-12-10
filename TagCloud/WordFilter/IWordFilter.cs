namespace TagCloud.WordFilter
{
    public interface IWordFilter
    {
        bool IsPermitted(string word);
    }
}
