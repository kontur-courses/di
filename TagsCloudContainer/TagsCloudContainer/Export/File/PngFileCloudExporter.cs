using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace TagsCloudContainer.Export.File;

public class PngFileCloudExporter : FileCloudExporter<PngFileCloudExporterOptions>
{
    public PngFileCloudExporter(PngFileCloudExporterOptions options) : base(options)
    {
    }

    protected override Task DoExportAsync(Image image, CancellationToken cancellationToken = default)
    {
        return image.SaveAsPngAsync(Options.FilePath, cancellationToken);
    }
}

public class JpegFileCloudExporter : FileCloudExporter<JpegFileCloudExporterOptions>
{
    public JpegFileCloudExporter(JpegFileCloudExporterOptions options) : base(options)
    {
    }

    protected override Task DoExportAsync(Image image, CancellationToken cancellationToken = default)
    {
        var encoder = new JpegEncoder
        {
            Quality = Options.Quality
        };
        return image.SaveAsJpegAsync(Options.FilePath, encoder, cancellationToken);
    }
}

public class JpegFileCloudExporterOptions : FileCloudExporterOptions
{
    public int? Quality { get; set; } = 95;
}