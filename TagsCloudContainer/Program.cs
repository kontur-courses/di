using System.IO;
using Autofac;
using NHunspell;
using TagsCloudContainer.Parsing;
using TagsCloudContainer.RectangleTranslation;
using TagsCloudContainer.Settings_Providing;
using TagsCloudContainer.Vizualization;
using TagsCloudContainer.Vizualization.Interfaces;
using TagsCloudContainer.Word_Counting;

namespace TagsCloudContainer
{
    public class Program
    {
        private static IContainer container;

        public static void Main(string[] args)
        {
            SetDependencies();
            using (var scope = container.BeginLifetimeScope())
            {
                var layouter = scope.Resolve<ICloudLayouter>();
                layouter.Layout(Path.Combine("..","..","example.txt"), Path.Combine("..","..","example.png"));
            }
        }

        private static void SetDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtParser>().As<IFileParser>();
            builder.RegisterType<SizeTranslator>().As<ISizeTranslator>();
            builder.RegisterType<WordFilter>().As<IWordFilter>();
            builder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            builder.RegisterType<WordCounter>().As<IWordCounter>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<CircularCloudLayouter>().As<IWordLayouter>();
            builder.RegisterType<CircularCloudVisualizer>().As<IVisualizer>();
            builder.RegisterInstance(new Hunspell(
                GetDictionaryDirectoryPath("index.aff"),
                GetDictionaryDirectoryPath("index.dic"))).As<Hunspell>();
            builder.RegisterInstance(new SettingsProvider()).As<SettingsProvider>();
            builder.Register(c => c.Resolve<SettingsProvider>().GetSettings()).As<Settings>();
            builder.Register(c => c.Resolve<Settings>().ColoringOptions).As<ColoringOptions>();
            builder.RegisterType<PngSaver>().As<ISaver>();
            container = builder.Build();
        }

        private static string GetDictionaryDirectoryPath(string filename)
        {
            return Path.Combine("..","..", "Dictionaries", filename);
        }
    }
}