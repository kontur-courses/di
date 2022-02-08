using SixLabors.ImageSharp;

namespace TagsCloudContainer.Export.File;

public abstract class FileCloudExporter<TOptions> : BaseCloudExporter<TOptions>
    where TOptions : FileCloudExporterOptions
{
    protected FileCloudExporter(TOptions options) : base(options)
    {
    }

    public override Task ExportAsync(Image image, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(Options.FilePath))
        {
            throw new InvalidOperationException("Path to export file is empty!");
        }

        return DoExportAsync(image, cancellationToken);
    }

    protected abstract Task DoExportAsync(Image image, CancellationToken cancellationToken = default);
}