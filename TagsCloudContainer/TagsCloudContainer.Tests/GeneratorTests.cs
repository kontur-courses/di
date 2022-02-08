using System.IO;
using System.Threading.Tasks;
using Codeuctivity;
using FluentAssertions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TagsCloudContainer.Export.File;
using TagsCloudContainer.Load.File;
using TagsCloudContainer.Processing;
using TagsCloudContainer.Render;
using TagsCloudContainer.Render.CircularCloud;
using Xunit;

namespace TagsCloudContainer.Tests;

public class GeneratorTests
{
    [Fact]
    public async Task Generate()
    {
        var options = new CircularCloudRenderOptions
        {
            TextColors = new[] {Color.Aqua},
            MinimumFontSize = 96
        };
        var render = new CircularCloudRender(options, new CircularCloudLayouter(options, new LogarithmSpiral(options)));
        var processor = new SimpleWordsProcessor(new SimpleWordsProcessorOptions());
        var loader = new FileWordsLoader(new FileWordsLoaderOptions
        {
            FilePath = Path.Combine("Data", "test.txt")
        });
        var path = Path.GetTempFileName();
        var exporter = new PngFileCloudExporter(new PngFileCloudExporterOptions
        {
            FilePath = path
        });
        var generator = new TagsCloudGenerator(render, processor, loader, exporter);

        var image = await generator.GenerateAsync();
        var expected = Image.Load<Rgba32>("Data/test2.png");
        ImageSharpCompare.ImagesAreEqual((Image<Rgba32>) image, expected).Should().BeTrue();
    }
    
    [Fact]
    public async Task GenerateAndExport()
    {
        var options = new CircularCloudRenderOptions
        {
            TextColors = new[] {Color.Aqua},
            MinimumFontSize = 96
        };
        var render = new CircularCloudRender(options, new CircularCloudLayouter(options, new LogarithmSpiral(options)));
        var processor = new SimpleWordsProcessor(new SimpleWordsProcessorOptions());
        var loader = new FileWordsLoader(new FileWordsLoaderOptions
        {
            FilePath = Path.Combine("Data", "test.txt")
        });
        var path = Path.GetTempFileName();
        var exporter = new PngFileCloudExporter(new PngFileCloudExporterOptions
        {
            FilePath = path
        });
        var generator = new TagsCloudGenerator(render, processor, loader, exporter);

        await generator.GenerateAndExportAsync();
        File.Exists(path);
        var expected = Image.Load<Rgba32>("Data/test2.png");
        var saved = Image.Load<Rgba32>(path);
        ImageSharpCompare.ImagesAreEqual(saved, expected).Should().BeTrue();
        File.Delete(path);
    }
}