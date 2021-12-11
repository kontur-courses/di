namespace TagsCloudContainer.Interfaces;

public interface IFilter
{
    bool Allows(string word);
}
