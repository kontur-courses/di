using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
using TagCloud.App;
using TagCloud.App.GUI;
using TagCloud.Infrastructure.Graphics;
using TagCloud.Infrastructure.Layout;
using TagCloud.Infrastructure.Layout.Environment;
using TagCloud.Infrastructure.Layout.Strategies;
using TagCloud.Infrastructure.Settings;
using TagCloud.Infrastructure.Settings.UISettingsManagers;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Conveyors;
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
            builder.RegisterType<TagCloudGenerator>().As<IImageGenerator>();

            builder.RegisterType<LowerCaseConveyor>().As<IConveyor<string>>();
            var myStemPath = GetReleasePath("mystem");
            builder.RegisterType<WordTypeConveyor>()
                .As<IConveyor<string>>()
                .WithParameter(new TypedParameter(typeof(string), myStemPath));
            builder.RegisterType<WordCounterConveyor>().As<IConveyor<string>>();
            builder.RegisterType<WordThresholdConveyor>().As<IConveyor<string>>();
            builder.RegisterType<InterestingWordsConveyor>().As<IConveyor<string>>();
            builder.RegisterType<WordFontSizeConveyor>().As<IConveyor<string>>();
            builder.RegisterType<WordSizeConveyor>().As<IConveyor<string>>();

            builder.RegisterType<Settings>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<PlainEnvironment>().AsImplementedInterfaces();
            builder.RegisterType<SpiralStrategy>().As<ILayoutStrategy>();
            builder.RegisterType<TagCloudLayouter>().As<ILayouter<Size, Rectangle>>();

            builder.RegisterType<WordPainter>().As<IPainter<string>>();
            builder.RegisterType<Random>().SingleInstance();
            builder.RegisterType<ColorPicker>();

            builder.RegisterType<FileSettingManager>().AsImplementedInterfaces();
            builder.RegisterType<ImagePathSettingManager>().AsImplementedInterfaces();
            builder.RegisterType<ImageSizeSettingsManager>().AsImplementedInterfaces();
            builder.RegisterType<LayoutCenterSettingManager>().AsImplementedInterfaces();
            builder.RegisterType<SpiralIncrementSettingManager>().AsImplementedInterfaces();
            builder.RegisterType<FontSettingManager>().AsImplementedInterfaces();
            builder.RegisterType<ImageFormatSettingManager>().AsImplementedInterfaces();

            builder.RegisterType<TagCloudGenerator>().As<IImageGenerator>();

            //todo use compile options
            // builder.RegisterType<TagCloudLayouterCli>().As<IApp>();
            builder.RegisterType<TagCloudLayouterGui>().As<IApp>();

            var container = builder.Build();
            var app = container.Resolve<IApp>();
            app.Run();
        }

        public static Settings GetDefaultSettings()
        {
            var size = new Size(500, 500);
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
                Format = ImageFormat.Bmp,
            };
        }

        public static string GetReleasePath(string filename)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "Release", filename);
        }
    }
}