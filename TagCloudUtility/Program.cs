using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Autofac;
using TagCloud.Models;
using TagCloud.PointsSequence;
using TagCloud.RectanglePlacer;
using TagCloud.Utility.Models.Tag;
using TagCloud.Utility.Models.TextReader;
using TagCloud.Utility.Models.WordFilter;
using CommandLine;
using TagCloud.Layouter;
using TagCloud.Utility.Data;
using TagCloud.Utility.Models.Tag.Container;
using TagCloud.Visualizer;
using TagCloud.Visualizer.Settings;

namespace TagCloud.Utility
{
    public static class Program
    {
        private static IContainer container;
        private static ILogger logger;

        private static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(o => Start(o, new Logger()));
        }

        public static void Start(Options options, ILogger logger)
        {
            Program.logger = logger;
            Program.logger.Log(options);
            if (container == null)
                container = ContainerConfig.Configure();
            try
            {
                Run(options);
            }
            catch (Exception e)
            {
                Program.logger.Log(e);
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
                new TypedParameter(typeof(Brush), new SolidBrush(ColorTranslator.FromHtml(options.Color)))
            );

            var visualizer = container.Resolve<ICloudVisualizer>(
                new TypedParameter(typeof(IDrawSettings), drawSettings)
            );

            var tagContainer = container.Resolve<ITagContainer>();
            if (options.PathToTags != null)
            {
                var tagContainerReader = container.Resolve<ITagContainerReader>(
                    new TypedParameter(typeof(string), Path.GetExtension(options.PathToTags)));
                tagContainer = tagContainerReader.ReadTagsContainer(Helper.GetPath(options.PathToTags));
            }

            var tagReader = container.Resolve<ITagReader>(new TypedParameter(typeof(ITagContainer), tagContainer));

            var wordFilter = container.Resolve<IWordFilter>();

            if (options.PathToStopWords != null)
            {
                var stopWordsTextReader =
                    container.ResolveNamed<ITextReader>(Path.GetExtension(options.PathToStopWords));
                var stopWords = stopWordsTextReader.ReadToEnd(Helper.GetPath(options.PathToStopWords));
                foreach (var stopWord in stopWords)
                    wordFilter.Add(stopWord);
            }

            var wordsReader = container.ResolveNamed<ITextReader>(Path.GetExtension(options.PathToWords));
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