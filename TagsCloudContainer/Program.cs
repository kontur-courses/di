using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using TagsCloudContainer.FrequencyAnalyzers;
using TagsCloudContainer.TextTools;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public static class Program
    {
        public static void Main()
        {
            var services = DependencyInjectionConfig.AddCustomServices(new ServiceCollection());
            var serviceProvider = services.BuildServiceProvider();

            var reader = serviceProvider.GetRequiredService<TextFileReader>();
            var analyzer = serviceProvider.GetRequiredService<FrequencyAnalyzer>();

            string text = reader.ReadText("sample.txt");
            analyzer.Analyze(text);



            //var cloudBuilder = new TagCloudBuilder();
            var drawingSettings = new CloudDrawingSettings("Arial", 8, new List<Color> { Color.AliceBlue });

            var layouter = new TagsCloudLayouter(new Point(300, 300), new SpiralPointsProvider(new Point(300, 300)), drawingSettings, analyzer.GetAnalyzedText());

            layouter.ToImage().Save("cloud.png");

            //var font = new Font(drawingSettings)

            //analyzer.SaveToFile("frequency.txt");

            //ServiceCollection collection = new();
            //collection.AddScoped<IFileReader, TextFileReader>();
            //collection.AddScoped<FrequencyAnalyzer>();

            //using ServiceProvider provider = collection.BuildServiceProvider();

            //IFileReader reader = provider.GetService<IFileReader>();
            //FrequencyAnalyzer analyzer = provider.GetService<FrequencyAnalyzer>();

            ////var text = reader.ReadTextFromFile("sample.txt");
            //analyzer.Analyze(reader.ReadTextFromFile("sample.txt"));
            //analyzer.SaveToFile("frequency.txt");
        }
    }
}