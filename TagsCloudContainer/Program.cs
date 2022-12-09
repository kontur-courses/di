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

            var myConfig = configuration.Get<MyConfiguration>();
            MyConfigValidator.ValidateConfig(myConfig);

            var container = new ServiceCollection()
                .AddSingleton<IConverter, FileToDictionaryConverter>()
                .AddSingleton<IWordsFilter, WordsFilter>()
                .AddSingleton<ISpiralDrawer, SpiralDrawer>()
                .AddSingleton<ILayout, CircularCloudLayout>()
                .AddSingleton<IWordSizeCalculator, WordSizeCalculator>()
                .AddSingleton<CloudDrawer>()
                .BuildServiceProvider();

            var drawer = ActivatorUtilities.CreateInstance<CloudDrawer>(container);

            drawer.DrawCloud(myConfig);
        }
    }
}