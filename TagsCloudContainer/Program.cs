using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CloudLayout;

namespace TagsCloudContainer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json", optional: false)
                .AddCommandLine(args)
                .Build();

            var options = configuration.Get<CustomOptions>();
            CustomOptionsValidator.ValidateOptions(options);

            var container = new ServiceCollection()
                .AddSingleton<IConverter, FileToDictionaryConverter>()
                .AddSingleton<IWordsFilter, WordsFilter>()
                .AddSingleton<ISpiralDrawer, SpiralDrawer>()
                .AddSingleton<ILayout, CircularCloudLayout>()
                .AddSingleton<IWordSizeCalculator, WordSizeCalculator>()
                .AddSingleton<CloudDrawer>()
                .BuildServiceProvider();

            var drawer = ActivatorUtilities.CreateInstance<CloudDrawer>(container);

            drawer.DrawCloud(options);
        }
    }
}