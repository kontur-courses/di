using System;
using System.Drawing;
using System.Linq;
using Autofac;
using TagCloud.Models;
using TagCloud.PointsSequence;
using TagCloud.RectanglePlacer;
using TagCloud.Utility.Models.Tag;
using TagCloud.Utility.Models.TextReader;
using TagCloud.Utility.Models.WordFilter;
using TagCloud.Layouter;
using TagCloud.Utility.Data;
using TagCloud.Utility.Models.Tag.Container;
using TagCloud.Visualizer;
using TagCloud.Visualizer.Settings;
using TagCloud.Visualizer.Settings.Colorizer;

namespace TagCloud.Utility
{
    public static class TagCloudProgram
    {
        private static IContainer container;
        private static ILogger logger;

        public static void Start(Options options, ILogger logger)
        {
            if (options == null)
                throw new ArgumentNullException($"{nameof(options)} was null");
            TagCloudProgram.logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} was null");
            TagCloudProgram.logger.Log(options);

            try
            {
                Helper.CheckPaths(options);

                if (container == null)
                    container = ContainerConfig.Configure();

                Run(options);
            }
            catch (Exception e)
            {
                TagCloudProgram.logger.Log(e);
            }
        }

        private static void Run(Options options)
        {
            var layouter = container
                .Resolve<ICloudLayouter>(
                    new TypedParameter(typeof(IRectanglePlacer), container.Resolve<IRectanglePlacer>()),
                    new TypedParameter(typeof(IPointsSequence), container.Resolve<IPointsSequence>())
                );

            var drawSettings = container.Resolve<IDrawSettings>(
                new TypedParameter(typeof(DrawFormat), options.DrawFormat),
                new TypedParameter(typeof(Font), new FontConverter().ConvertFrom(options.Font)),
                new TypedParameter(typeof(IColorizer),
                    container.Resolve<IColorizer>(new TypedParameter(typeof(Color),
                        ColorTranslator.FromHtml(options.Brush)))),
                new TypedParameter(typeof(Color), ColorTranslator.FromHtml(options.Color))
            );

            var visualizer = container.Resolve<ICloudVisualizer>(
                new TypedParameter(typeof(IDrawSettings), drawSettings)
            );

            var tagContainer = container.Resolve<ITagContainer>();
            if (options.PathToTags != null)
            {
                var tagContainerReader =
                    container.ResolveNamed<ITagContainerReader>(Helper.GetExtension(options.PathToTags));
                tagContainer = tagContainerReader.ReadTagsContainer(Helper.GetPath(options.PathToTags));
            }

            var tagReader = container.Resolve<ITagReader>(new TypedParameter(typeof(ITagContainer), tagContainer));

            var wordFilter = container.Resolve<IWordFilter>();

            if (options.PathToStopWords != null)
            {
                var stopWordsTextReader =
                    container.ResolveNamed<ITextReader>(Helper.GetExtension(options.PathToStopWords));
                var stopWords = stopWordsTextReader.ReadToEnd(Helper.GetPath(options.PathToStopWords));
                foreach (var stopWord in stopWords)
                    wordFilter.Add(stopWord);
            }

            var wordsReader = container.ResolveNamed<ITextReader>(Helper.GetExtension(options.PathToWords));
            var words = wordsReader
                .ReadToEnd(Helper.GetPath(options.PathToWords));

            var filteredWords = wordFilter
                .FilterWords(words);
            var tags = tagReader.ReadTags(filteredWords);
            var cloudItems = tags
                .Select(tag => new CloudItem(tag.Word, layouter.PutNextRectangle(tag.Size)))
                .ToArray();

            var picture = visualizer.CreatePictureWithItems(cloudItems);
            if (options.Size != null)
                picture = Helper.ResizeImage(picture, options.Size);
            picture.Save(Helper.GetPath(options.PathToPicture), Helper.GetImageFormat(options.PathToPicture));
            logger.Log(cloudItems);
            logger.Log(picture);
        }
    }
}