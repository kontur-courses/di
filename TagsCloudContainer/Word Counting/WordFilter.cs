namespace TagsCloudContainer.Word_Counting
{
    public class WordFilter : IWordFilter
    {
        public bool IsExcluded(string word)
        {
            return false;
        }
    }
}