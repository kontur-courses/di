using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.App.CloudGenerator;
using TagsCloudContainer.App.CloudVisualizer;
using TagsCloudContainer.App.DataReader;
using TagsCloudContainer.App.TextParserToFrequencyDictionary;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.DataReader;
using TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary;

namespace TagsCloudContainer.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var inputFile = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "..", "..", "..", "text.txt"));
            var outputFile = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "..", "..", "..", "cloud.png"));
            var imageSettings = new ImageSettings(
                new Size(500, 500), "Arial", Color.Black, ImageFormat.Png);
            var services = new ServiceCollection()
                .AddSingleton<IDataReader>(new TxtFileReader(inputFile))
                .AddScoped<ITextParser, SimpleTextParser>()
                .AddScoped<IWordNormalizer, ToLowerWordNormalizer>()
                .AddScoped<IWordFilter, SimpleWordFilter>()
                .AddScoped<ITextParserToFrequencyDictionary, TextParserToFrequencyDictionary.TextParserToFrequencyDictionary>()
                .AddSingleton<IFontGetter> (new FontGetter(imageSettings.FontName))
                .AddScoped<ICloudGenerator, CloudGenerator.CloudGenerator>()
                .AddSingleton<IImageGenerator> (new ImageGenerator(imageSettings))
                .AddSingleton<ICloudLayouter>(new CircularCloudLayouter(new Point(250, 250)))
                .AddScoped<ICloudVisualizer, CloudVisualizer.CloudVisualizer>();
            var serviceProvider = services.BuildServiceProvider();
            var visualizer = serviceProvider.GetService<ICloudVisualizer>();
            var image = visualizer.Visualize(inputFile, imageSettings);
            image.Save(outputFile, ImageFormat.Png);
        }
    }
}