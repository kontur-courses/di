using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Autofac;
using Fclp;
using TagCloud.Data;
using TagCloud.Processor;
using TagCloud.Reader;
using TagCloud.RectanglesLayouter.PointsGenerator;

namespace TagCloud
{
    public class Program
    {
        private static readonly string Help =
            "Program to generate tag cloud\n" +
            $"USAGE: {AppDomain.CurrentDomain.FriendlyName} -w WordsFile -b BoringWordsFile -i ResultImageName " +
            "[-m FontSizeMultiplier] [-c WordsColor] [-g BackgroundColor] [-f FontFamily]\n";

        private static readonly Dictionary<string, Brush> BrushesByName = typeof(Brushes)
            .GetProperties()
            .ToDictionary(
                propertyInfo => propertyInfo.Name,
                propertyInfo => (Brush)propertyInfo.GetValue(null, null));

        public static void Main(string[] args)
        {
            if (!TryGetArguments(args, out var arguments))
                return;

            var builder = new ContainerBuilder();
            SetUpContainer(builder);
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

            var result = parser.Parse(args);

            if (TryGetBrush(textBrushName, out var textBrush))
                newArguments.WordsBrush = textBrush;
            else
                return false;

            if (TryGetBrush(backgroundBrushName, out var backgroundBrush))
                newArguments.BackgroundBrush = backgroundBrush;
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

        private static bool TryGetBrush(string textBrushName, out Brush brush)
        {
            brush = null;
            if (BrushesByName.TryGetValue(textBrushName, out var backgroundBrush))
            {
                brush = backgroundBrush;
                return true;
            }
            Console.WriteLine($"Unknown Brush {textBrushName}");
            return false;
        }

        public static void SetUpContainer(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register(c => new Point()).As<Point>();
            builder.Register(c =>  new SpiralPointsGenerator(1, 0.01)).As<IPointsGenerator>();
            builder.RegisterType<TextFileReader>().As<IWordsFileReader>();
            builder.RegisterType<RussianWordsProcessor>().As<IWordsProcessor>();
        }
    }
}