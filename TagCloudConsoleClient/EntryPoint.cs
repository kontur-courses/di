using System.Drawing;
using System.Linq;
using Autofac;
using CommandLine;
using TagsCloudContainer;
using TagsCloudVisualization;

namespace TagCloudConsoleClient
{
    internal static class EntryPoint
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<TagCloudConfig>(args).WithParsed(DrawTagCloud);
        }

        private static void DrawTagCloud(TagCloudConfig config)
        {
            var wordColor = Color.FromName(config.TextColor);
            var bgColor = Color.FromName(config.BackgroundColor);

            var container = BuildContainer();
            var wordLayouts = container.Resolve<TagCloudBuilder>()
                .Build(config.InputFilePath, wordColor, bgColor, config.TextSize);
            container.Resolve<TagCloudSaver>().Save(bgColor, wordLayouts.ToList(), config.OutputFilePath);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleColorPicker>().As<IWordColorPicker>();
            builder.RegisterType<SimpleFontPicker>().As<IWordFontPicker>();
            builder.RegisterType<TextFileWordsProvider>().As<IFileWordsProvider>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<BitmapSaver>().As<IBitmapSaver>();

            builder.RegisterType<TagCloudSaver>().AsSelf();

            builder.RegisterType<TagCloudBuilder>().AsSelf();
            builder.RegisterType<WordPreprocessor>().AsSelf();
            return builder.Build();
        }
    }
}
