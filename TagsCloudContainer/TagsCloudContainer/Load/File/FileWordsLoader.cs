namespace TagsCloudContainer.Load.File;

public class FileWordsLoader : BaseWordsLoader<FileWordsLoaderOptions>
{
    public FileWordsLoader(FileWordsLoaderOptions options) : base(options)
    {
    }

    public override async Task<IEnumerable<string>> GetWordsAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(Options.FilePath))
        {
            throw new InvalidOperationException("File path is empty");
        }

        return await System.IO.File.ReadAllLinesAsync(Options.FilePath, cancellationToken);
    }
}