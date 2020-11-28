using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.App.CloudGenerator;
using TagsCloudContainer.App.CloudVisualizer;
using TagsCloudContainer.App.FileReader;
using TagsCloudContainer.App.FrequencyDictionaryGenerator;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddScoped<IFileReader, TxtFileReader>()
                .AddScoped<ITextParser, SimpleTextParser>()
                .AddScoped<IWordNormalizer, ToLowerWordNormalizer>()
                .AddScoped<IWordFilter, SimpleWordFilter>()
                .AddScoped<IFrequencyDictionaryGenerator, FrequencyDictionaryGenerator.FrequencyDictionaryGenerator>()
                .AddScoped<IFontSizeGetter, FontSizeGetter>()
                .AddScoped<ICloudGenerator, CloudGenerator.CloudGenerator>()
                .AddScoped<IImageGenerator, ImageGenerator>()
                .AddSingleton<ICloudLayouter>(new CircularCloudLayouter(new Point(250, 250)))
                .AddScoped<ICloudVisualizer, CloudVisualizer.CloudVisualizer>();
            var serviceProvider = services.BuildServiceProvider();
            var visualizer = serviceProvider.GetService<ICloudVisualizer>();
            var imageSettings = new ImageSettings(
                new Size(500, 500), "Arial", Color.Black, ImageFormat.Png);
            var inputFile = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "..", "..", "..", "text.txt"));
            var outputFile = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "..", "..", "..", "cloud.png"));
            var image = visualizer.Visualize(inputFile, imageSettings);
            image.Save(outputFile, ImageFormat.Png);
        }
    }
}