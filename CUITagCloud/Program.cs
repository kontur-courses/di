using System.Collections.Generic;
using System.Drawing;
using Autofac;
using CommandLine;
using TagsCloudContainer.CloudBuilder;
using TagsCloudContainer.CloudDrawers;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudLayouters.PointGenerators;
using TagsCloudContainer.CloudTagController;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.Settings;
using TagsCloudContainer.TextParsers;
using TagsCloudContainer.WordConverter;

namespace CUITagCloud
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Option>(args).WithParsed(DrawCloud);
        }

        private static void DrawCloud(Option options)
        {
            var container = BuildContainer(options);
            var cloudTagController = container.Resolve<ICloudTagController>();
                
            cloudTagController.Work();
        }

        private static IContainer BuildContainer(Option options)
        {
            
            var imageSettings = new ImageSettings(options.Height, options.Width, options.OutputFile, options.Theme);
            var fileSettings = new FileSettings(options.InputFileName);
            var filterSettings = new FilterSettings(options.BoringWordsFileName, options.SmallestLength);
            var textSettings = new TextSettings(options.CountWords, options.Filters, options.Converters, filterSettings);
            
            var builder = new ContainerBuilder();

            builder.RegisterInstance(fileSettings).As<FileSettings>();
            builder.RegisterInstance(imageSettings).As<ImageSettings>();
            builder.RegisterInstance(textSettings).As<TextSettings>();

            builder.RegisterType<TextFileReader>().As<IFileReader>();
            builder.RegisterType<InitialFormWordConverter>().As<IWordConverter>();
            builder.RegisterType<TextParser>().As<ITextParser>();
            builder.RegisterType<TagsCloudBuilder>().As<ICloudBuilder>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
            builder.RegisterType<ArchimedesSpiralPointGenerator>().As<IEnumerable<Point>>();
            builder.RegisterType<CloudTagController>().As<ICloudTagController>();
                
            return builder.Build();
        }
    }
}