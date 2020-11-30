namespace TagsCloudVisualisation.Text.Preprocessing
{
    public class EmptyWordFilter : IWordFilter
    {
        public bool IsValidWord(string word) => true;
    }
}