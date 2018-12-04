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
using TagCloud.Visualizer;
using TagCloud.Visualizer.Settings;

namespace TagCloud.Utility
{
    public static class Program
    {
        private static IContainer container;
        private static ILogger log;

        static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(o => Start(o, new Logger()));
        }

        public static void Start(Options options, ILogger logger)
        {
            log = logger;
            container = ContainerConfig.Configure();
            try
            {
                Run(options);
            }
            catch (Exception e)
            {
                logger.Log(e);
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
                new TypedParameter(typeof(Font),new FontConverter().ConvertFrom(options.Font)),
                new TypedParameter(typeof(Brush), new SolidBrush(ColorTranslator.FromHtml(options.Color)))
            );

            var visualizer = container.Resolve<ICloudVisualizer>(
                new TypedParameter(typeof(IDrawSettings), drawSettings)
            );

            var tagGroups = options.PathToTags != null
                ? Helper.ReadTagsContainer(new StreamReader(Helper.GetPath(options.PathToTags)).ReadToEnd())
                : container.Resolve<TagContainer>();

            var tagReader = container
                .Resolve<ITagReader>(
                    new TypedParameter(typeof(TagContainer), tagGroups)
                );
            var textReader = container.ResolveNamed<ITextReader>(options.WordsFileType);
            var wordFilter = container.Resolve<IWordFilter>();
            if (options.PathToStopWords != null)
                foreach (var stopWord in textReader.ReadToEnd(Helper.GetPath(options.PathToStopWords)))
                    wordFilter.AddStopWord(stopWord);

            var words = textReader
                .ReadToEnd(Helper.GetPath(options.PathToWords));

            var filteredWords = wordFilter
                .FilterWords(words);

            var cloudItems = tagReader
                .GetTags(filteredWords)
                .Select(tag => new CloudItem(tag.Word, layouter.PutNextRectangle(tag.Size)))
                .ToArray();

            var picture = visualizer.CreatePictureWithItems(cloudItems);
            if (options.Size != null)
                picture = Helper.ResizeImage(picture, options.Size);
            picture.Save(Helper.GetPath(options.PathToPicture));

            log.Log(cloudItems);
            log.Log(picture);
        }
    }
}