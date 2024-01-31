namespace TagsCloudContainer.Algorithm
{
    public interface IFileParser
    {
        List<string> ReadWordsInFile(string filePath);
    }
}