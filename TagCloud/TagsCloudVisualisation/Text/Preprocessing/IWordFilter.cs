namespace TagsCloudVisualisation.Text.Preprocessing
{
    public interface IWordFilter
    {
        bool IsValidWord(string word);
    }
}