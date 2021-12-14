using System;
using System.Drawing;
using System.Linq;
using Autofac;
using CloudImageSaver;
using CommandLine;
using TagsCloudVisualization;
using TagsCloudVisualization.Settings;

namespace TagsCloudCLI
{ 
    class Program
    { 
        static void Main(string[] args)
        {
            try
            {
                var result = Parser.Default.ParseArguments<Options>(args);
                if (result.Errors.Any())
                    return;
                var settings = new SettingProvider().GetSettings(result.Value);
                Run(settings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Run(GeneralSettings settings)
        {
            var saver = new ImageSaver(settings.Saver.Directory, settings.Saver.ImageName);
            var builder = new ContainerBuilder();
            var container = builder.Build();
            builder.RegisterModule(new TagsCloudModule(settings));
            var image = container.Resolve<Visualizer>().Visualize();
            saver.Save(image);
        }
    }
}