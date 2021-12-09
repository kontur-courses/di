using System;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudApp.RenderCommand;

namespace TagsCloudApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<RenderOptions>(args).Value;
            var services = new RenderServicesConfigurator(options);
            var provider = services.ConfigureServices().BuildServiceProvider();
            try
            {
                var renderCommand = provider.GetRequiredService<IRenderCommand>();
                renderCommand.Render();
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}