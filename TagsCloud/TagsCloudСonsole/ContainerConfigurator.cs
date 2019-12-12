using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Autofac;
using DocoptNet;
using TagsCloudTextProcessing.Filters;
using TagsCloudTextProcessing.Formatters;
using TagsCloudTextProcessing.Readers;
using TagsCloudTextProcessing.Shufflers;
using TagsCloudTextProcessing.Tokenizers;
using TagsCloudTextProcessing.WordsIntoTokensTranslators;
using TagsCloudVisualization.BitmapSavers;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Styling;
using TagsCloudVisualization.Styling.TagColorizer;
using TagsCloudVisualization.Styling.TagSizeCalculators;
using TagsCloudVisualization.Styling.Themes;
using TagsCloudVisualization.Visualizers;

namespace TagsCloudConsole
{
    public static class ContainerConfigurator
    {
        public static IContainer Configure(IDictionary<string, ValueObject> parameters)
        {
            var builder = new ContainerBuilder();
            var applicationParameters = typeof(Application).GetConstructors()[0].GetParameters();
            builder
                .RegisterType<Application>()
                .WithParameter(applicationParameters[0].Name, parameters["--w"].ToString())
                .WithParameter(applicationParameters[1].Name, parameters["--h"].ToString())
                .WithParameter(applicationParameters[2].Name, parameters["--output"].ToString());

            //text reading configure
            ConfigureTextReader(parameters["--input"].ToString(), parameters["--input_ext"].ToString(), builder);

            //text processing configure
            var patternName = typeof(Tokenizer).GetConstructors()[0].GetParameters()[0].Name;
            builder.RegisterType<Tokenizer>()
                .As<ITokenizer>()
                .WithParameter(patternName, parameters["--split_pattern"].ToString());
            builder.RegisterType<FormatterLowercaseAndTrim>().As<IWordsFormatter>();
            ConfigureWordsFilter(parameters["--exclude"].ToString(), builder);
            builder.RegisterType<WordsIntoTokenTranslator>().As<IWordsIntoTokenTranslator>();

            //style and visualize configure
            ConfigureShuffler(parameters["--shuffle"].ToString(), parameters["--seed"], builder);
            ConfigureFontProperties(parameters["--font"].ToString(), parameters["--font_size"], builder);
            ConfigureTheme(parameters["--theme"].ToString(), builder);
            builder.RegisterType<LogarithmicTagSizeCalculator>().As<TagSizeCalculator>();
            ConfigureColorizer(parameters["--colorize"].ToString(), builder);
            builder.RegisterType<TextNoRectanglesVisualizer>().As<ICloudVisualizer>();
            ConfigureSpiral(
                parameters["--x"].ToString(),
                parameters["--y"].ToString(),
                parameters["--rad"].ToString(),
                parameters["--incr"].ToString(),
                parameters["--angle"].ToString(),
                builder);
            builder.RegisterType<SpiralLayouter>().As<ICloudLayouter>();

            //configure bitmap saver
            ConfigureSaver(parameters["--output_ext"].ToString(), builder);
            return builder.Build();
        }

        private static void ConfigureWordsFilter(string wordsToExcludePath, ContainerBuilder builder)
        {
            var wordsToExclude = new[] {""};
            if (wordsToExcludePath != "none")
                wordsToExclude = File.ReadAllLines(wordsToExcludePath);
            var excludeFromListFilterParameters = typeof(ExcludeFromListFilter).GetConstructors()[0].GetParameters();
            builder.RegisterType<ExcludeFromListFilter>()
                .As<IWordsFilter>()
                .WithParameter(excludeFromListFilterParameters[0].Name, wordsToExclude);
        }

        private static void ConfigureSaver(string extension, ContainerBuilder builder)
        {
            var saverType = typeof(PngBitmapSaver);
            if (extension == "wmf")
                saverType = typeof(WmfBitmapSaver);
            else if (extension == "jpeg")
                saverType = typeof(JpegBitmapSaver);

            builder.RegisterType(saverType)
                .As<IBitmapSaver>();
        }

        private static void ConfigureSpiral(string x, string y, string radius, string increment,
            string angle, ContainerBuilder builder)
        {
            float.TryParse(x, out var xFloat);
            float.TryParse(y, out var yFloat);
            if (!float.TryParse(radius, out var radiusFloat))
                radiusFloat = 0.5f;
            if (!float.TryParse(increment, out var incrementFloat))
                incrementFloat = 0.5f;
            float.TryParse(angle, out var angleFloat);
            builder
                .RegisterInstance(new Spiral(new PointF(xFloat, yFloat), radiusFloat, incrementFloat, angleFloat))
                .SingleInstance();
        }

        private static void ConfigureColorizer(string colorizeMethod, ContainerBuilder builder)
        {
            var colorizerType = typeof(RandomTagColorizer);
            if (colorizeMethod == "s")
                colorizerType = typeof(BySizeTagColorizer);

            builder.RegisterType(colorizerType)
                .As<ITagColorizer>();
        }

        private static void ConfigureTheme(string themeName, ContainerBuilder builder)
        {
            var themeType = typeof(PixelArtTheme);
            if (themeName == "gr")
                themeType = typeof(GrayDarkTheme);
            else if (themeName == "go")
                themeType = typeof(GodotEngineTheme);

            builder.RegisterType(themeType)
                .As<ITheme>();
        }

        private static void ConfigureFontProperties(string font, ValueObject fontSize, ContainerBuilder builder)
        {
            var size = 16;
            if (fontSize.IsInt)
                size = fontSize.AsInt;
            builder.RegisterInstance(new FontProperties(font, size)).SingleInstance();
        }

        private static void ConfigureTextReader(string path, string extension, ContainerBuilder builder)
        {
            var readerType = typeof(TxtTextReader);
            if (extension == "docx")
                readerType = typeof(DocxTextReader);
            else if (extension == "pdf")
                readerType = typeof(PdfTextReader);

            var pathName = typeof(TxtTextReader).GetConstructors()[0].GetParameters()[0].Name;

            builder.RegisterType(readerType)
                .As<ITextReader>()
                .WithParameter(pathName, path);
        }

        private static void ConfigureShuffler(string shuffleType, ValueObject seed, ContainerBuilder builder)
        {
            switch (shuffleType)
            {
                case "a":
                    builder.RegisterType<AscendingCountShuffler>()
                        .As<ITokenShuffler>();
                    break;
                case "d":
                    builder.RegisterType<DescendingCountShuffler>()
                        .As<ITokenShuffler>();
                    break;
                default:
                {
                    var randomSeedName = typeof(RandomShuffler).GetConstructors()[0].GetParameters()[0].Name;
                    var randomSeed = Environment.TickCount;
                    if (seed.IsInt)
                        randomSeed = seed.AsInt;
                    builder.RegisterType<RandomShuffler>()
                        .As<ITokenShuffler>()
                        .WithParameter(randomSeedName, randomSeed);
                    break;
                }
            }
        }
    }
}