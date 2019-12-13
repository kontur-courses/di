using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using CommandLine;
using NHunspell;
using TagsCloudContainer.Parsing;
using TagsCloudContainer.RectangleTranslation;
using TagsCloudContainer.Settings_Providing;
using TagsCloudContainer.Visualization;
using TagsCloudContainer.Visualization.Interfaces;
using TagsCloudContainer.Word_Counting;

namespace TagsCloudContainer
{
    public class Program
    {
        private static IContainer container;

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Execute)
                .WithNotParsed(HandleExceptions);
        }

        private static void Execute(Options options)
        {
            SetDependencies(options);
            var layouter = container.Resolve<ICloudLayouter>();
            layouter.Layout(options.InputPath, options.OutputPath);
        }

        private static void SetDependencies(Options options)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtParser>().As<IFileParser>();
            builder.RegisterType<SizeTranslator>().As<ISizeTranslator>();
            builder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            builder.RegisterType<CircularCloudLayouter>().As<IWordLayouter>();
            builder.RegisterInstance(new Hunspell(
                GetDictionaryDirectoryPath("index.aff"),
                GetDictionaryDirectoryPath("index.dic"))).As<Hunspell>();
            builder.RegisterType<PngSaver>().As<ISaver>();
            builder.Register(c => new SettingsProvider(options, c.Resolve<IFileParser>()))
                .As<SettingsProvider>();
            builder.Register(c => c.Resolve<SettingsProvider>().GetSettings()).As<Settings>();
            builder.Register(c => c.Resolve<SettingsProvider>().GetColoringOptions()).As<ColoringOptions>();
            
            builder.Register(c =>
            {
                var settings = c.Resolve<Settings>();
                return new CircularCloudVisualizer(settings.ColoringOptions, c.Resolve<ISaver>(), settings.Resolution,
                    settings.FontName);
            }).As<IVisualizer>();
            builder.Register(c =>
            {
                var settings = c.Resolve<Settings>();
                return new WordFilter(settings.ExcludedWords, settings.ExcludedPartsOfSpeech);
            }).As<IWordFilter>();
            builder.RegisterType<WordCounter>().As<IWordCounter>();
            builder.RegisterType<CloudLayouter>().As<ICloudLayouter>();
            container = builder.Build();
        }

        private static string GetDictionaryDirectoryPath(string filename)
        {
            return Path.Combine("..", "..", "Dictionaries", filename);
        }

        private static void HandleExceptions(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                throw new ArgumentException(error.ToString());
            }
        }
    }
}