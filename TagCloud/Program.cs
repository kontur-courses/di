using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
            "[-m FontSizeMultiplier] [-c WordsColor] [-g BackgroundColor] [-f FontFamily] [-s]\n\n-s\tsave to clipboard\n";

        private static readonly Dictionary<string, Color> BrushesByName = typeof(Color)
            .GetProperties()
            .Where(color => color.PropertyType == typeof(Color))
            .ToDictionary(
                propertyInfo => propertyInfo.Name,
                propertyInfo => (Color) propertyInfo.GetValue(null, null));

        [STAThread]
        public static void Main(string[] args)
        {
            if (!TryGetArguments(args, out var arguments))
                return;

            var builder = new ContainerBuilder();
            SetUpContainer(builder, arguments);
            var container = builder.Build();

            container.Resolve<TagCloudGenerator>().Generate(arguments);  
        }

        private static bool TryGetArguments(string[] args, out Arguments arguments)
        {
            arguments = null;
            var newArguments = new Arguments();

            var textBrushName = "Black";
            var backgroundBrushName = "White";

            var parser = new FluentCommandLineParser();
            parser.Setup<string>('w').Callback(file => newArguments.WordsFileName = file).Required();
            parser.Setup<string>('b').Callback(boring => newArguments.BoringWordsFileName = boring).Required();
            parser.Setup<string>('i').Callback(name => newArguments.ImageFileName = name).Required();
            parser.Setup<string>('c').Callback(color => textBrushName = color);
            parser.Setup<string>('g').Callback(color => backgroundBrushName = color);
            parser.Setup<string>('f').Callback(font => newArguments.FontFamily = new FontFamily(font));
            parser.Setup<int>('m').Callback(size => newArguments.Multiplier = size);
            parser.Setup<bool>('s').Callback(save => newArguments.ToEnableClipboardSaver = save);

            var result = parser.Parse(args);

            if (TryGetBrush(textBrushName, out var textBrush))
                newArguments.WordsColor = textBrush;
            else
                return false;

            if (TryGetBrush(backgroundBrushName, out var backgroundBrush))
                newArguments.BackgroundColor = backgroundBrush;
            else
                return false;

            if (result.HasErrors)
            {
                Console.WriteLine("Wrong syntax\n");
                Console.WriteLine(Help);
                return false;
            }

            arguments = newArguments;
            return true;
        }

        private static bool TryGetBrush(string textBrushName, out Color brush)
        {
            brush = new Color();
            if (BrushesByName.TryGetValue(textBrushName, out var backgroundBrush))
            {
                brush = backgroundBrush;
                return true;
            }
            Console.WriteLine($"Unknown Brush {textBrushName}");
            return false;
        }

        public static void SetUpContainer(ContainerBuilder builder, Arguments arguments)
        {
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type != typeof(ClipboardImageSaver) || arguments.ToEnableClipboardSaver)
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register(c => new Point()).As<Point>();
            builder.Register(c =>  new SpiralPointsGenerator(1, 0.01)).As<IPointsGenerator>();
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