using SixLabors.ImageSharp;

namespace TagsCloudContainer.Export;

public interface ICloudExporter
{
    Task ExportAsync(Image image, CancellationToken cancellationToken = default);
}