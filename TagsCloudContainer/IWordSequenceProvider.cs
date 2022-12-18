namespace TagsCloudContainer;

public interface IWordSequenceProvider
{
    public Result<IEnumerable<string>> WordSequence { get; }
}