namespace TagsCloudContainer.Processing;

public abstract class BaseWordsProcessor<TOptions> : IWordsProcessor where TOptions : WordsProcessorOptions
{
    protected TOptions Options { get; }

    protected BaseWordsProcessor(TOptions options)
    {
        Options = options;
    }

    public bool TryProcess(string word, out string processedWord)
    {
        processedWord = word.Trim().ToLowerInvariant();
        if (IsBoringWord(processedWord))
        {
            return false;
        }

        return true;
    }

    protected virtual bool IsBoringWord(string processedWord)
    {
        return Options.BoringWords.Contains(processedWord);
    }
}