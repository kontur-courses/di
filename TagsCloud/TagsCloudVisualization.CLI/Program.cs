using System;
using System.IO;
using System.Linq;
using Autofac;
using CommandLine;
using TagsCloudDrawer.ImageCreator;
using TagsCloudVisualization.DrawableFactory;
using TagsCloudVisualization.WordsPreprocessor;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagsTransformers;

namespace TagsCloudVisualization.CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.OfType<HelpRequestedError>().Any()) return;
            if (result.Errors.Any()) throw new ArgumentException(result.Errors.First().ToString());

            TagsCloudDrawerModuleSettings settings;
            try
            {
                settings = result.Value.ToDrawerSettings();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudDrawerModule(settings));
            var container = builder.Build();

            var directory = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedClouds");
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            var filename = Path.Combine(directory, DateTime.Now.Ticks.ToString());
            var preprocessor = container.Resolve<IWordsPreprocessor>();
            var words = container.Resolve<IWordsProvider>().GetWords();
            var processedWords = preprocessor.Process(words);
            var tags = container.Resolve<IWordsToTagsTransformer>().Transform(processedWords);
            var drawables = tags
                            .OrderByDescending(tag => tag.Weight)
                            .Select(container.Resolve<ITagDrawableFactory>().Create);
            container.Resolve<IImageCreator>().Create(filename, drawables);
            Console.WriteLine($"Tags cloud {filename} generated.");
        }
    }
}