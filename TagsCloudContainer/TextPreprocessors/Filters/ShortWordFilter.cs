namespace TagsCloudContainer.TextPreprocessors.Filters
{
    public class ShortWordFilter : IWordFilter
    {
        public bool Filter(string word)
        {
            return word.Length > 4;
        }
    }
}