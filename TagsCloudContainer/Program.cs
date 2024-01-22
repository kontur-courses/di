using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.FreqAnalyzer;
using TagsCloudContainer.TextTools;

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

            string text = reader.ReadTextFromFile("sample.txt");
            analyzer.Analyze(text);

            analyzer.SaveToFile("frequency.txt");

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