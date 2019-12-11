using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using DocoptNet;
using TagsCloudTextProcessing.Excluders;
using TagsCloudTextProcessing.Formatters;
using TagsCloudTextProcessing.Readers;
using TagsCloudTextProcessing.Shufflers;
using TagsCloudTextProcessing.Splitters;
using TagsCloudTextProcessing.Tokenizers;
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
            builder
                .RegisterType<Application>()
                .WithParameter("wordsToExcludePath", parameters["--exclude"].ToString())
                .WithParameter("width", parameters["--w"].ToString())
                .WithParameter("height", parameters["--h"].ToString())
                .WithParameter("imageOutputPath", parameters["--output"].ToString());

            //text reading configure
            ConfigureTextReader(parameters["--input"].ToString(), parameters["--input_ext"].ToString(), builder);

            //text processing configure
            builder.RegisterType<TextSplitter>()
                .As<ITextSplitter>()
                .WithParameter("splitPatter", parameters["--split_pattern"].ToString());
            builder.RegisterType<WordsFormatterLowercaseAndTrim>().As<IWordsFormatter>();
            builder.RegisterType<WordsExcluder>().As<IWordsExcluder>();
            builder.RegisterType<Tokenizer>().As<ITokenizer>();

            //style and visualize configure
            ConfigureShuffler(parameters["--shuffle"].ToString(), parameters["--seed"], builder);
            ConfigureFontProperties(parameters["--font"].ToString(), parameters["--font_size"], builder);
            ConfigureTheme(parameters["--theme"].ToString(), builder);
            builder.RegisterType<TagSizeCalculatorLogarithmic>().As<TagSizeCalculator>();
            ConfigureColorizer(parameters["--colorize"].ToString(), builder);
            builder.RegisterType<TextNoRectanglesVisualizer>().As<ICloudVisualizer>();
            ConfigureSpiral(
                parameters["--x"].ToString(),
                parameters["--y"].ToString(),
                parameters["--rad"].ToString(),
                parameters["--incr"].ToString(),
                parameters["--angle"].ToString(),
                builder);
            builder.RegisterType<SpiralCloudLayouter>().As<ICloudLayouter>();

            //configure bitmap saver
            ConfigureSaver(parameters["--output_ext"].ToString(), builder);
            return builder.Build();
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
            var colorizerType = typeof(TagColorizerRandom);
            if (colorizeMethod == "s")
                colorizerType = typeof(TagColorizerBySize);

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

            builder.RegisterType(readerType)
                .As<ITextReader>()
                .WithParameter("path", path);
        }

        private static void ConfigureShuffler(string shuffleType, ValueObject seed, ContainerBuilder builder)
        {
            switch (shuffleType)
            {
                case "a":
                    builder.RegisterType<TokenShufflerAscending>()
                        .As<ITokenShuffler>();
                    break;
                case "d":
                    builder.RegisterType<TokenShufflerDescending>()
                        .As<ITokenShuffler>();
                    break;
                default:
                {
                    var randomSeed = Environment.TickCount;
                    if (seed.IsInt)
                        randomSeed = seed.AsInt;
                    builder.RegisterType<TokenShufflerRandom>()
                        .As<ITokenShuffler>()
                        .WithParameter("randomSeed", randomSeed);
                    break;
                }
            }
        }
    }
}