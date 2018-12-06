namespace TagCloud.Interfaces
{
    public interface IWordFilter
    {
        bool ToExclude(string word);
    }
}