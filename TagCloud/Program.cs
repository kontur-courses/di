using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Autofac;
using Fclp;
using TagCloud.ColorPicker;
using TagCloud.Counter;
using TagCloud.Data;
using TagCloud.Drawer;
using TagCloud.Processor;
using TagCloud.Reader;
using TagCloud.Reader.FormatReader;
using TagCloud.RectanglesLayouter;
using TagCloud.RectanglesLayouter.PointsGenerator;
using TagCloud.Saver;
using TagCloud.WordsLayouter;

namespace TagCloud
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var output = new StringBuilder();
            var arguments = TryGetArguments(args, output);

            if (output.Length > 0)
            {
                Console.WriteLine(output);
                return;
            }

            var builder = new ContainerBuilder();
            SetUpContainer(builder, arguments);
            var container = builder.Build();
            container.Resolve<TagCloudGenerator>().Generate(arguments);
        }

        private static Arguments TryGetArguments(string[] args, StringBuilder output)
        {
            var arguments = new Arguments();

            var parser = new FluentCommandLineParser();
            parser
                .SetupHelp("h", "?", "help")
                .Callback(str => output.Append(str))
                .WithHeader("Program to create tag cloud. Options:");
            parser
                .Setup<string>('w')
                .Callback(file => arguments.WordsFileName = file)
                .WithDescription("Name of file with words");
            parser
                .Setup<string>('b')
                .Callback(boring => arguments.BoringWordsFileName = boring)
                .WithDescription("Name of file with boring words");
            parser
                .Setup<string>('i')
                .Callback(name => arguments.ImageFileName = name)
                .WithDescription("Name of result image (png, jpg, bmp)");
            parser
                .Setup<string>('c')
                .Callback(color => arguments.WordsColorName = color)
                .WithDescription("Words color");
            parser
                .Setup<string>('g')
                .Callback(color => arguments.BackgroundColorName = color)
                .WithDescription("Background color");
            parser
                .Setup<string>('f')
                .Callback(font => arguments.FontFamilyName = font)
                .WithDescription("Words font family");
            parser
                .Setup<int>('m')
                .Callback(size => arguments.Multiplier = size)
                .WithDescription("Words font size multiplier");
            parser
                .Setup<bool>('s')
                .Callback(save => arguments.ToEnableClipboardSaver = true)
                .WithDescription("Save image to clipboard");

            parser.Parse(args);

            CheckArguments(arguments, output);

            return arguments;
        }

        private static void CheckArguments(Arguments arguments, StringBuilder output)
        {
            CheckFile("Words", arguments.WordsFileName, output);
            CheckFile("Boring words", arguments.BoringWordsFileName, output);

            var format = TextFileReader.GetFormat(arguments.ImageFileName);
            CheckArgument(FileImageSaver.Formats.Keys, "image format", format, output);

            CheckArgument(CloudDrawer.Colors, "words color", arguments.WordsColorName, output);
            CheckArgument(CloudDrawer.Colors, "background color", arguments.BackgroundColorName, output);
            CheckArgument(CloudWordsLayouter.Fonts, "font", arguments.FontFamilyName, output);

            if (arguments.Multiplier <= 0)
                output.AppendLine("Font size multiplier should be positive");
        }

        private static void CheckFile(string argumentName, string fileName, StringBuilder output)
        {
            if (!File.Exists(fileName))
                output.AppendLine($"{argumentName} file not found {fileName}");
        }

        private static void CheckArgument<T>(ICollection<T> variants, string argumentName, T argument, StringBuilder output)
        {
            if (!variants.Contains(argument))
                output.AppendLine($"Unknown {argumentName} {argument}");
        }

        public static void SetUpContainer(ContainerBuilder builder, Arguments arguments)
        {
            builder.RegisterType<TagCloudGenerator>().AsSelf();

            builder.RegisterType<CloudWordsLayouter>().As<IWordsLayouter>();
            builder.RegisterType<CloudDrawer>().As<IWordsDrawer>();
            builder.RegisterType<TextFileReader>().As<IWordsFileReader>();
            builder.RegisterType<WordsCounter>().As<IWordsCounter>();
            builder.RegisterType<FileImageSaver>().As<IImageSaver>();
            builder.RegisterType<ClipboardImageSaver>().As<IImageSaver>();

            builder.RegisterType<CircularCloudLayouter>().As<IRectangleLayouter>();
            builder.Register(c => new Point()).As<Point>();
            builder.Register(c => new SpiralPointsGenerator(1, 0.01)).As<IPointsGenerator>();

            builder.RegisterType<DocxReader>().As<IFormatReader>();
            builder.RegisterType<BrightnessColorPicker>().As<IColorPicker>();

            builder.RegisterType<WordsToLowerProcessor>().As<IWordsProcessor>();
            builder
                .Register(c => new BoringWordsProcessor(c
                    .Resolve<IWordsFileReader>()
                    .ReadWords(arguments.BoringWordsFileName)))
                .As<IWordsProcessor>();
            builder.RegisterType<StemWordsProcessor>().As<IWordsProcessor>();
        }
    }
}