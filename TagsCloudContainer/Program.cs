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

            var reader = serviceProvider.GetService<TextFileReader>();
            var analyzer = serviceProvider.GetService<FrequencyAnalyzer>();

            string text = reader.ReadText("sample.txt");
            analyzer.Analyze(text);


            var center = new Point(300, 300);

            var pointsProvider = new SpiralPointsProvider(center);

            var drawingSettings = new CloudDrawingSettings("Arial", 8, new List<Color> { Color.AliceBlue });

            var layouter = new TagsCloudLayouter(center, pointsProvider, drawingSettings, analyzer.GetAnalyzedText());

            layouter.ToImage().Save("cloud.png");
        }
    }
}