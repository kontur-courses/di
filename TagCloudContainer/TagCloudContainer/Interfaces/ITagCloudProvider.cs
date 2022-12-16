namespace TagCloudContainer;

public interface ITagCloudProvider
{
    public IEnumerable<Word> GetPreparedWords();
}