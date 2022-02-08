using SixLabors.ImageSharp;

namespace TagsCloudContainer.Export;

public abstract class BaseCloudExporter<TOptions> : ICloudExporter where TOptions : BaseCloudExporterOptions
{
    protected TOptions Options { get; }

    protected BaseCloudExporter(TOptions options)
    {
        Options = options;
    }

    public abstract Task ExportAsync(Image image, CancellationToken cancellationToken = default);
}