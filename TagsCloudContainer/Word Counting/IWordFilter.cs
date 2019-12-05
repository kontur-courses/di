namespace TagsCloudContainer.Word_Counting
{
    public interface IWordFilter
    {
        bool IsExcluded(string word);
    }
}