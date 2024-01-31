using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.TagsCloud;
using TagsCloudContainer.Utility;

namespace TagsCloudContainer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .WithParsed(o =>
                    {
                        using (var serviceProvider = Startup.ConfigureServices())
                        {
                            var tagCloudApp = serviceProvider.GetRequiredService<TagCloudApp>();
                            tagCloudApp.Run(o);
                        }
                    });
        }                
    }
}
