using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagCloud;

namespace TagCloudApplication;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(option =>
            {
                using var serviceProvider = TagCloudGenerator.ConfigureService(option).BuildServiceProvider();
                serviceProvider.GetService<TagCloudGenerator>().Generate();
            });
    }
}