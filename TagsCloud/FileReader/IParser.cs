namespace TagsCloud;

public interface IParser
{
    public string FileType { get; }

    public IEnumerable<string?> GetWordList(string filePath);
}