using System;
using System.IO;
using System.Linq;
using Autofac;
using TagsCloudDrawer.ImageCreator;
using TagsCloudVisualization.DrawableFactory;
using TagsCloudVisualization.WordsPreprocessor;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagsTransformers;

namespace TagsCloudVisualization
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TagsCloudDrawerModule(new TagsCloudDrawerModuleSettings
            {
                WordsFile = Path.Combine(Directory.GetCurrentDirectory(), "words.txt")
            }));
            var container = builder.Build();

            var directory = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedClouds");
            var filename = Path.Combine(directory, DateTime.Now.Ticks.ToString());
            var preprocessor = container.Resolve<IWordsPreprocessor>();
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            var words = container.Resolve<IWordsProvider>().GetWords();
            var processedWords = preprocessor.Process(words);
            var tags = container.Resolve<IWordsToTagsTransformer>().Transform(processedWords);
            var drawables = tags
                            .OrderByDescending(tag => tag.Weight)
                            .Select(container.Resolve<ITagDrawableFactory>().Create);
            container.Resolve<IImageCreator>().Create(filename, drawables);
        }
    }
}