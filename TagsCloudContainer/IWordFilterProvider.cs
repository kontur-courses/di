namespace TagsCloudContainer;

public interface IWordFilterProvider
{
    public Result<IEnumerable<string>> WordFilter { get; }
}