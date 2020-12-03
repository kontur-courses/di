using System.Drawing;
using System.Linq;
using Autofac;
using McMaster.Extensions.CommandLineUtils;
using TagCloud.Interfaces;
using TagsCloudVisualization;

namespace TagCloud
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WordsNormalizer>().As<IWordsNormalizer>();
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloudLayouter>();
            builder.RegisterType<SpiralPointsFactory>().As<IPointsFactory>();
            builder.RegisterType<ColorGeneratorFactory>().As<IColorGeneratorFactory>();
            builder.RegisterType<CloudDrawerFactory>().As<ICloudDrawerFactory>();
            builder.RegisterType<WordsReader>().As<IWordsReader>();
            builder.RegisterType<TagCloudLayouterFactory>().As<ITagCloudLayouterFactory>();
            builder.RegisterType<WordsForCloudGeneratorFactory>().As<IWordsForCloudGeneratorFactory>();
            builder.RegisterType<TagCloudCreatorFactory>().As<ITagCloudCreatorFactory>();


            var tagCloudCreatorFactory = builder.Build().Resolve<ITagCloudCreatorFactory>();


            ParsArguments(args, tagCloudCreatorFactory);
        }

        private static void ParsArguments(string[] args, ITagCloudCreatorFactory tagCloudCreatorFactory)
        {
            var app = new CommandLineApplication();
            app.HelpOption();

            var pictureSizeArg = app.Option<int>("-s|--pictureSize <int,int>", "Picture size",
                CommandOptionType.MultipleValue);
            var cloudCenterArg = app.Option<int>("-c|--cloudCenter <int,int>", "Cloud center",
                CommandOptionType.MultipleValue);
            var colorsArgs =
                app.Option<string>("-o|--colors <name,name,...>", "Colors", CommandOptionType.MultipleValue);
            var fontNameArg = app.Option<string>("-f|--font <name>", "font", CommandOptionType.SingleValue);
            var maxFontSizeArg =
                app.Option<int>("-m|--maxFontSize <int>", "Max font size", CommandOptionType.SingleValue);
            var inputFileArg =
                app.Option<string>("-n|--inputFile <path>", "Input file .txt", CommandOptionType.SingleValue);
            var outputFileArg =
                app.Option<string>("-u|--outputFile <path>", "output file", CommandOptionType.SingleValue);
            var boringWordsFileArg = app.Option<string>("-b|--boringWords <path>", "boring words file",
                CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var pictureSize = pictureSizeArg.HasValue() && pictureSizeArg.Values.Count == 2
                    ? new Size(pictureSizeArg.ParsedValues[0], pictureSizeArg.ParsedValues[1])
                    : new Size(2000, 2000);

                var cloudCenter = cloudCenterArg.HasValue() && cloudCenterArg.Values.Count == 2
                    ? new Point(cloudCenterArg.ParsedValues[0], cloudCenterArg.ParsedValues[1])
                    : new Point(1000, 1000);

                var colors = colorsArgs.HasValue()
                    ? cloudCenterArg.Values.Select(Color.FromName).ToArray()
                    : new[] {Color.Black};

                var fontName = fontNameArg.HasValue()
                    ? fontNameArg.ParsedValue
                    : "Arial";

                var maxFontSize = maxFontSizeArg.HasValue()
                    ? maxFontSizeArg.ParsedValue
                    : 40;

                var inputFile = inputFileArg.HasValue()
                    ? inputFileArg.ParsedValue
                    : "in.txt";

                var outputFile = outputFileArg.HasValue()
                    ? outputFileArg.ParsedValue
                    : "out.png";

                var boringWordsFile = boringWordsFileArg.HasValue()
                    ? boringWordsFileArg.ParsedValue
                    : "boring.txt";

                CreateTagCloud(tagCloudCreatorFactory,
                    pictureSize,
                    cloudCenter,
                    colors,
                    fontName,
                    maxFontSize,
                    inputFile,
                    boringWordsFile,
                    outputFile);
            });
            app.Execute(args);
        }

        private static void CreateTagCloud(ITagCloudCreatorFactory tagCloudCreatorFactory, Size pictureSize,
            Point cloudCenter, Color[] colors, string fontName,
            int maxFontSize, string inputFile, string boringWordsFile, string outputFile)
        {
            var bitmap = GetCloudImage(tagCloudCreatorFactory,
                pictureSize,
                cloudCenter,
                colors,
                fontName,
                maxFontSize,
                inputFile,
                boringWordsFile);

            bitmap.Save(outputFile);
        }

        private static Bitmap GetCloudImage(ITagCloudCreatorFactory tagCloudCreatorFactory, Size pictureSize,
            Point cloudCenter, Color[] colors, string fontName,
            int maxFontSize, string inputFile, string boringWordsFile)
        {
            var tagCloudCreator = tagCloudCreatorFactory
                .Get(pictureSize,
                    cloudCenter,
                    colors,
                    fontName,
                    maxFontSize,
                    inputFile,
                    boringWordsFile);
            var bitmap = tagCloudCreator.GetCloud();
            return bitmap;
        }
    }
}