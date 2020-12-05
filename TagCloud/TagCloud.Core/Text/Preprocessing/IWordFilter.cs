namespace TagCloud.Core.Text.Preprocessing
{
    public interface IWordFilter
    {
        bool IsValidWord(string word);
    }
}