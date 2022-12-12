using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization.Configurations;
using TagsCloudVisualization.Enums;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<Options>(args).Value;
            options.RunOptions();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<CloudConfiguration>((x) => CloudConfiguration.Default);
            serviceCollection.AddSingleton<DistributionConfiguration>((x) => DistributionConfiguration.Default);
            serviceCollection.AddScoped<ICloudLayouter, CircularCloudLayouter>();
            serviceCollection.AddScoped<ICloudCreator, TagCloudCreator>();

            var provider = serviceCollection.BuildServiceProvider();

            var lines = File.ReadAllLines(options.WordsFilePath);
            
            var words = Preprocessor.Process(lines, 
                PartSpeech.Noun | PartSpeech.Adjective);
            
            var bitmaps = provider.GetService<ICloudCreator>()!.Create(words, 100).ToList();

            for (var i = 0; i < bitmaps.Count; i++)
                bitmaps[i].Save(options.SaveTagCloudImagePath + $"cloud_{i}.{ImageFormat.Png}");
        }
    }
}