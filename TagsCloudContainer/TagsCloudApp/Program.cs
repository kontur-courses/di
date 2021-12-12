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
            var options = Parser.Default.ParseArguments<RenderArgs>(args).Value;
            if (options == null)
                return;

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