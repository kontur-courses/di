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
        private static readonly string Help =
            "Program to generate tag cloud\n" +
            $"USAGE: {AppDomain.CurrentDomain.FriendlyName} -w WordsFile -b BoringWordsFile -i ResultImageName " +
            "[-m FontSizeMultiplier] [-c WordsColor] [-g BackgroundColor] [-f FontFamily] [-s]\n\n" +
            "\t-s\tsave to clipboard\n";

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
            var exceptions = new StringBuilder();
            var arguments = TryGetArguments(args, exceptions);

            if (exceptions.Length > 0)
            {
                Console.WriteLine("One or more errors found:\n");
                Console.WriteLine(exceptions);
                Console.WriteLine(Help);
                return;
            }

            var builder = new ContainerBuilder();
            SetUpContainer(builder, arguments);
            var container = builder.Build();
            container.Resolve<TagCloudGenerator>().Generate(arguments);
        }

        private static Arguments TryGetArguments(string[] args, StringBuilder exceptions)
        {
            var arguments = new Arguments();

            var parser = new FluentCommandLineParser();
            parser.Setup<string>('w').Callback(file => arguments.WordsFileName = file).Required();
            parser.Setup<string>('b').Callback(boring => arguments.BoringWordsFileName = boring).Required();
            parser.Setup<string>('i').Callback(name => arguments.ImageFileName = name).Required();
            parser.Setup<string>('c').Callback(color => arguments.WordsColorName = color);
            parser.Setup<string>('g').Callback(color => arguments.BackgroundColorName = color);
            parser.Setup<string>('f').Callback(font => arguments.FontFamilyName = font);
            parser.Setup<int>('m').Callback(size => arguments.Multiplier = size);
            parser.Setup<bool>('s').Callback(save => arguments.ToEnableClipboardSaver = true);

            var result = parser.Parse(args);

            CheckArguments(result, arguments, exceptions);

            return arguments;
        }

        private static void CheckArguments(ICommandLineParserResult result, Arguments arguments, StringBuilder exceptions)
        {
            if (result.HasErrors)
            {
                var errors = result.ErrorText.Split(new[] {"\r\n"}, StringSplitOptions.None);
                foreach (var error in errors)
                    exceptions.AppendLine("\t" + error);
            }

            CheckFile("Words", arguments.WordsFileName, exceptions);
            CheckFile("Boring words", arguments.BoringWordsFileName, exceptions);

            CheckFileExtension(TextFileReader.FormatReaders.Keys, arguments.WordsFileName, "text", exceptions);
            CheckFileExtension(TextFileReader.FormatReaders.Keys, arguments.BoringWordsFileName, "text", exceptions);
            CheckFileExtension(FileImageSaver.Formats.Keys, arguments.ImageFileName, "image", exceptions);

            CheckArgument(Colors, "words color", arguments.WordsColorName, exceptions);
            CheckArgument(Colors, "background color", arguments.BackgroundColorName, exceptions);
            CheckArgument(Fonts, "font", arguments.FontFamilyName, exceptions);

            if (arguments.Multiplier <= 0)
                exceptions.AppendLine("\tFont size multiplier should be positive");
        }

        private static void CheckFileExtension(ICollection<string> variants, string fileName, string formatName, StringBuilder exceptions)
        {
            var format = Regex.Match(fileName, ".+\\.(.+)$").Groups[1].Value;
            CheckArgument(variants, $"{formatName} format", format, exceptions);
        }

        private static void CheckFile(string argumentName, string fileName, StringBuilder exceptions)
        {
            if (!File.Exists(fileName))
                exceptions.AppendLine($"\t{argumentName} file not found {fileName}");
        }

        private static void CheckArgument<T>(ICollection<T> variants, string argumentName, T argument, StringBuilder exceptions)
        {
            if (!variants.Contains(argument))
                exceptions.AppendLine($"\tUnknown {argumentName} {argument}");
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