using System;
using System.Linq;
using Autofac;
using CommandLine;
using TagsCloudVisualization;

namespace TagsCloudCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            HandleErrors(() =>
            {
                var result = Parser.Default.ParseArguments<Options>(args);
                if (result.Errors.Any()) 
                    return;
                var settings = new SettingProvider().GetSettings(result.Value);
                var builder = new ContainerBuilder();
                builder.RegisterModule(new TagsCloudDrawerModule(settings));
                var container = builder.Build();
                container.Resolve<Visualizer>().Visualize();
            });
        }
        
        private static void HandleErrors(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}