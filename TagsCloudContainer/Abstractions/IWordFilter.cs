namespace TagsCloudContainer.Abstractions;

public interface IWordFilter
{
    bool IsValid(string word);
}