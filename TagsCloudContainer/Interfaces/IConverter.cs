namespace TagsCloudContainer.Interfaces
{
    public interface IConverter
    {
        Dictionary<string, int> GetWordsInFile(ICustomOptions options);
    }
}