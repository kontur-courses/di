namespace TagsCloudContainer.WordFilter
{
    public class NotNullWordFilter : IFilter
    {
        public bool Validate(string word)
        {
            return !string.IsNullOrEmpty(word);
        }
    }
}