namespace TagCloud.Interfaces
{
    public interface IWordExcluder
    {
        bool ToExclude(string word);
    }
}