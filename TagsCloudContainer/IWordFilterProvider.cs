namespace TagsCloudContainer;

public interface IWordFilterProvider
{
    public IEnumerable<string> WordFilter { get; }
}