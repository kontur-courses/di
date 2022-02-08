namespace TagsCloudContainer.Load;

public interface IWordsLoader
{
    Task<IEnumerable<string>> GetWordsAsync(CancellationToken cancellationToken);
}