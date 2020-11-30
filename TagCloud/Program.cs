using System;
using System.Drawing;
using System.IO;
using Autofac;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Layout;
using TagCloud.Infrastructure.Layout.Environment;
using TagCloud.Infrastructure.Layout.Strategies;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Filters;
using TagCloud.Infrastructure.Text.Tokens;

namespace TagCloud
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LineParser>().As<IParser<string>>();

            builder.RegisterType<ToLowerFilter>().As<IFilter<string>>();
            var myStemPath = GetReleasePath("mystem");
            builder.RegisterType<InterestingWordsFilter>()
                .As<IFilter<string>>()
                .WithParameter(new TypedParameter(typeof(string), myStemPath));
            builder.RegisterType<Settings>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
            
            builder.RegisterType<PlainEnvironment>().AsImplementedInterfaces();
            builder.RegisterType<SpiralStrategy>().As<ILayoutStrategy>();
            builder.RegisterType<TagCloudLayouter>().As<ILayouter<Size, Rectangle>>();
            
            builder.RegisterType<WordMeasurer>().As<ITokenMeasurer<string>>();
            builder.RegisterType<WordCounter>().As<ITokenCounter<string>>();
            builder.RegisterType<WordPainter>().As<IPainter<string>>();

            var container = builder.Build();
            
            //todo default settings option
            var settingsFactory = container.Resolve<Func<Settings>>();
            settingsFactory().ExcludedTypes = new []{"CONJ", "SPRO", "PR"};
            settingsFactory().Path = GetReleasePath("input.txt");
            settingsFactory().Increment = 1;
            var size  = new Size(1000, 1000);
            settingsFactory().Width = size.Width;
            settingsFactory().Height = size.Height;
            settingsFactory().MinFontSize = 20;
            settingsFactory().MaxFontSize = 50;
            settingsFactory().Center = new Point(size.Width/2, size.Height/2);;
            settingsFactory().ImagePath = Path.Combine(".","drawing.bmp");
            settingsFactory().FontFamily = new FontFamily("Arial");
            settingsFactory().Brush = new SolidBrush(Color.Red);

            var parser = container.Resolve<IParser<string>>();
            var tokens = parser.Parse();
            
            var counter = container.Resolve<ITokenCounter<string>>();
            var fontSizes = counter.GetFontSizes(tokens);
            
            var measurer = container.Resolve<ITokenMeasurer<string>>();
            var sizes = measurer.GetSizes(fontSizes);
            
            var painter = container.Resolve<IPainter<string>>();
            var image = painter.GetImage(sizes, fontSizes);

            var imagePath = settingsFactory().ImagePath;
            image.Save(imagePath);
            Console.WriteLine("Image saved");
            Console.WriteLine(Path.GetFullPath(imagePath));
        }

        private static string GetReleasePath(string filename)
        {
            return Path.Combine(".", "bin", "Release", filename);
        }
    }
}