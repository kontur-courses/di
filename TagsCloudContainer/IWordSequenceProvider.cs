namespace TagsCloudContainer;

public interface IWordSequenceProvider
{
    public IEnumerable<string> WordSequence { get; }
}