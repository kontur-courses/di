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
            ImageSettings imageSettings;
            FileSettings fileSettings;
            TextSettings textSettings;

            Parser.Default.ParseArguments<Option>(args).WithParsed(o =>
            {
                imageSettings = new ImageSettings(o.Height, o.Width, o.OutputFile, o.Theme);
                fileSettings = new FileSettings(o.InputFileName);
                textSettings = new TextSettings(o.CountWords, o.Filters, o.Converters, o.BoringWordsFileName);
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
                
                var container = builder.Build();
                var cloudTagController = container.Resolve<ICloudTagController>();
                
                cloudTagController.Work();
            });
        }
    }
}