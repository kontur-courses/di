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
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtReader>().As<IReader<string>>();
            builder.RegisterType<WordAnalyzer<string>>();
            
            builder.RegisterType<LowerCaseFilter>().As<IFilter<string>>();
            var myStemPath = GetReleasePath("mystem");
            builder.RegisterType<WordTypeFilter>()
                .As<IFilter<string>>()
                .WithParameter(new TypedParameter(typeof(string), myStemPath));
            builder.RegisterType<WordCounterFilter>().As<IFilter<string>>();
            builder.RegisterType<WordThresholdFilter>().As<IFilter<string>>();
            builder.RegisterType<InterestingWordsFilter>().As<IFilter<string>>();
            builder.RegisterType<WordFontSizeFilter>().As<IFilter<string>>();
            builder.RegisterType<WordSizeFilter>().As<IFilter<string>>();

            builder.RegisterType<Settings>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<PlainEnvironment>().AsImplementedInterfaces();
            builder.RegisterType<SpiralStrategy>().As<ILayoutStrategy>();
            builder.RegisterType<TagCloudLayouter>().As<ILayouter<Size, Rectangle>>();
            builder.RegisterType<WordPainter>().As<IPainter<string>>();

            // builder.RegisterType<TagCloudLayouterCli>().As<IApp>();
            builder.RegisterType<TagCloudLayouterGui>().As<IApp>();

            var container = builder.Build();
            var app = container.Resolve<IApp>();
            app.Run();
            
        }

        public static Settings GetDefaultSettings()
        {
            var size  = new Size(1000, 1000);
            return new Settings
            {
                ExcludedTypes = new[] {WordType.CONJ, WordType.SPRO, WordType.PR},
                Path = GetReleasePath("input.txt"),
                WordCountThreshold = 2,
                Increment = 1,
                Width = size.Width,
                Height = size.Height,
                MinFontSize = 5,
                MaxFontSize = 50,
                Center = new Point(size.Width / 2, size.Height / 2),
                ImagePath = Path.Combine(".", "drawing.bmp"),
                FontFamily = new FontFamily("Arial"),
                Brush = new SolidBrush(Color.Wheat)
            };
        }

        private static string GetReleasePath(string filename)
        {
            return Path.Combine(".", "bin", "Release", filename);
        }
    }
}