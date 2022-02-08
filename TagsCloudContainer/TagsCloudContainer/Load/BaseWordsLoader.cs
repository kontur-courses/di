namespace TagsCloudContainer.Load;

public abstract class BaseWordsLoader<TOptions> : IWordsLoader where TOptions : BaseWordsLoaderOptions
{
    protected TOptions Options { get; }

    protected BaseWordsLoader(TOptions options)
    {
        Options = options;
    }

    public abstract Task<IEnumerable<string>> GetWordsAsync(CancellationToken cancellationToken);
}