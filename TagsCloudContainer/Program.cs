using System.Drawing;
using Autofac;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.WordCounters;
using TagsCloudContainer.Palettes;
using TagsCloudContainer.Visualizers;
using CommandLine;
using TagsCloudContainer.WordPreprocessors;
using TagsCloudContainer.WordFilters;
using Autofac.Core;
using TagsCloudContainer.Readers;
using TagsCloudContainer.TokensAndSettings;
using TagsCloudContainer.TagsCloudGenerators;
using TagsCloudContainer.PaintersWords;
using System;

namespace TagsCloudContainer
{
    class Program
    {
        public class Options
        {
            [Option('r', "read", MetaValue = "FILE", Required = true, HelpText = "Full wordlist file name.")]
            public string File { get; set; }
            [Option('h', "height", Default = 1024, HelpText = "Image height.")]
            public int Height { get; set; }
            [Option('w', "width", Default = 1024, HelpText = "Image width.")]
            public int Width { get; set; }
            [Option('f', "font", Default = "Arial", HelpText = "Font name.")]
            public string Font { get; set; }
            [Option('s', "size", Default = 20, HelpText = "Font size.")]
            public int Size { get; set; }
            [Option('c', "color", Default = "Red", HelpText = "Font color. (Does not work)")]
            public string Color { get; set; }
            [Option('i', "image", Default = "image.png", HelpText = "Image name.")]
            public string Image { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       var containerBuilder = new ContainerBuilder();

                       containerBuilder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().WithParameter("center", new Point());
                       containerBuilder.RegisterType<SimpleWordPreprocessor>().As<IWordPreprocessor>();
                       containerBuilder.RegisterType<SimpleWordFilter>().As<IWordFilter>();
                       containerBuilder.RegisterType<SimpleWordCounter>().As<IWordCounter>();

                       containerBuilder.RegisterType<SimplePalette>().As<IPalette>()
                       .WithParameters(
                           new Parameter[]
                           {
                               new NamedParameter("font", new Font(o.Font, o.Size)),
                               new NamedParameter("painterWords", new SimplePainterWords(new SolidBrush(Color.FromName(o.Color))))
                           }
                           );
                       containerBuilder.RegisterType<SimpleVisualizer>().As<IVisualizer>().WithParameter("imageSettings", new ImageSettings(o.Height, o.Width));
                       containerBuilder.RegisterType<SimpleReader>().As<IReader>().WithParameter("path", o.File);

                       containerBuilder.RegisterType<TagsCloudGenerator>().As<TagsCloudGenerator>();

                       var container = containerBuilder.Build();
                       var tagsCloudGenerator = container.Resolve<TagsCloudGenerator>();

                       var bitmap = tagsCloudGenerator.CreateTagCloud();
                       bitmap.Save(o.Image);
                   });
        }
    }
}
