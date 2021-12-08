namespace TagsCloudContainer.Abstractions;

public interface ITextReader
{
    IEnumerable<string> ReadLines();
}