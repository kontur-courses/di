using System;
using System.Drawing;
using Autofac;
using TagsCloudVisualization.CloudPainters;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.PathFinders;
using TagsCloudVisualization.TextFilters;
using TagsCloudVisualization.TextPreprocessing;
using TagsCloudVisualization.TextReaders;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.WordConverters;
using TagsCloudVisualization.WordSizing;

namespace TagsCloudVisualization
{
    public class ContainerCreator
    {
        public IContainer GetContainer(ApplicationOptions.ApplicationOptions applicationOptions)
        {
            var imageOptions = applicationOptions.GetVisualizingOptions();
            var affPath = PathFinder.GetHunspellDictionariesPath("ru_RU.aff");
            var dicPath = PathFinder.GetHunspellDictionariesPath("ru_RU.dic");
            var center = new Point(imageOptions.ImageSize.Width / 2, imageOptions.ImageSize.Height / 2);
            var builder = new ContainerBuilder();
            if (applicationOptions.BoringWords != null)
            {
                var boringWords = new WordsExtractor().GetWords(applicationOptions.BoringWords);
                builder.RegisterType<BoringWordsFilter>().As<ITextFilter>()
                    .WithParameter("boringWords", boringWords);
            }
            else
            {
                builder.RegisterType<BoringWordsFilter>().As<ITextFilter>();
            }
            builder.RegisterType<SizedWord>().AsSelf();
            builder.RegisterType<MultiColorFrequencyCloudPainter>().As<ICloudPainter<Tuple<SizedWord, Rectangle>>>();
            builder.RegisterType<LowerCaseWordConverter>().As<IWordConverter>();
            builder.RegisterType<NormalFormWordConverter>().As<IWordConverter>()
                .WithParameter("affPath", affPath)
                .WithParameter("dicPath", dicPath);
            builder.RegisterType<ShortWordsFilter>().As<ITextFilter>();
            builder.RegisterType<TxtReader>().As<ITextReader>();
            builder.RegisterType<WordsExtractor>().AsSelf();
            builder.RegisterType<WordPreprocessor>().AsSelf();
            builder.RegisterType<TagCloudVisualizer>().AsSelf().WithParameter("visualisingOptions", imageOptions);
            builder.RegisterType<TagCloudSizedVisualizer>().AsSelf().WithParameter("visualisingOptions", imageOptions);
            builder.RegisterType<Spiral>().AsSelf().WithParameter("center", center);
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<CloudCreator>().AsSelf();
            return builder.Build();
        }
    }
}