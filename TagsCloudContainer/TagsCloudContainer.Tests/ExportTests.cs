using System.IO;
using System.Threading.Tasks;
using Codeuctivity;
using FluentAssertions;
using FluentAssertions.Equivalency;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TagsCloudContainer.Export.File;
using Xunit;

namespace TagsCloudContainer.Tests;

public class ExportTests
{
    [Fact]
    public async Task ExportToPng()
    {
        var path = Path.GetTempFileName();
        var exporter = new PngFileCloudExporter(new PngFileCloudExporterOptions
        {
            FilePath = path
        });
        var image = await Image.LoadAsync<Rgba32>("Data/test.png");
        await exporter.ExportAsync(image);
        File.Exists(path).Should().BeTrue();
        var saved = Image.Load<Rgba32>(path);
        ImageSharpCompare.ImagesAreEqual(saved, image).Should().BeTrue();
        File.Delete(path);
    }
}