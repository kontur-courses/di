namespace TagsCloudContainer.Abstractions;

public interface IWordNormalizer
{
    string? Normalize(string word);
}