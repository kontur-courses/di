namespace TagsCloudContainer.WordFilter
{
    public interface IFilter
    {
        bool Validate(string word);
    }
}