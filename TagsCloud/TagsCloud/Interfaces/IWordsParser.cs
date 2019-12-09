namespace TagsCloudGenerator.Interfaces
{
    public interface IWordsParser : IFactorial
    {
        string[] ParseFromFile(string filePath);
    }
}