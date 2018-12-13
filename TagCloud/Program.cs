using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Autofac;
using Fclp;
using TagCloud.ColorPicker;
using TagCloud.Data;
using TagCloud.Processor;
using TagCloud.Reader;
using TagCloud.RectanglesLayouter.PointsGenerator;
using TagCloud.Saver;

namespace TagCloud
{
    public class Program
    {
        private static readonly HashSet<string> Colors = new HashSet<string>(
            typeof(Color)
                .GetProperties()
                .Where(color => color.PropertyType == typeof(Color))
                .Select(color => color.Name));

        private static readonly HashSet<string> Fonts =
            new HashSet<string>(FontFamily.Families.Select(font => font.Name));

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

        private static void CheckArguments(Arguments arguments, StringBuilder exceptions)
        {
            CheckFile("Words", arguments.WordsFileName, exceptions);
            CheckFile("Boring words", arguments.BoringWordsFileName, exceptions);

            CheckFileExtension(TextFileReader.FormatReaders.Keys, arguments.WordsFileName, "text", exceptions);
            CheckFileExtension(TextFileReader.FormatReaders.Keys, arguments.BoringWordsFileName, "text", exceptions);
            CheckFileExtension(FileImageSaver.Formats.Keys, arguments.ImageFileName, "image", exceptions);

            CheckArgument(Colors, "words color", arguments.WordsColorName, exceptions);
            CheckArgument(Colors, "background color", arguments.BackgroundColorName, exceptions);
            CheckArgument(Fonts, "font", arguments.FontFamilyName, exceptions);

            if (arguments.Multiplier <= 0)
                exceptions.AppendLine("Font size multiplier should be positive");
        }

        private static void CheckFileExtension(ICollection<string> variants, string fileName, string formatName, StringBuilder output)
        {
            var format = Regex.Match(fileName, ".+\\.(.+)$").Groups[1].Value;
            CheckArgument(variants, $"{formatName} format", format, output);
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
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type != typeof(ClipboardImageSaver) || arguments.ToEnableClipboardSaver)
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register(c => new Point()).As<Point>();
            builder.Register(c => new SpiralPointsGenerator(1, 0.01)).As<IPointsGenerator>();
            builder.RegisterType<TextFileReader>().As<IWordsFileReader>();
            builder.RegisterType<BrightnessColorPicker>().As<IColorPicker>();
            builder
                .Register(c => new RussianWordsProcessor(c
                    .Resolve<IWordsFileReader>()
                    .Read(arguments.BoringWordsFileName)))
                .As<IWordsProcessor>();
        }
    }
}