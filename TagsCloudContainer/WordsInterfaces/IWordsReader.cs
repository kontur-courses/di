namespace TagsCloudContainer.WordsInterfaces;

public interface IWordsReader
{
    public List<string> Read(string? path);
}