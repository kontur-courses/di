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
                if (TagCloudServicesFactory.ConfigureServiceAndTryGet<TagCloudGenerator>(option, out var generator))
                    generator.Generate();
                else
                    throw new Exception("Can't configure service");
            });
    }
}