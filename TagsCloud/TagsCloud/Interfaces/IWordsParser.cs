namespace TagsCloudGenerator.Interfaces
{
    public interface IWordsParser
    {
        string[] ParseFromFile(string filePath);
    }
}