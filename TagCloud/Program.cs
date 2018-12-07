using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Autofac;
using Fclp;
using TagCloud.Data;
using TagCloud.RectanglesLayouter.PointsGenerator;

namespace TagCloud
{
    public class Program
    {
        private static readonly string Help =
            "Program to generate tag cloud\n" +
            $"USAGE: {AppDomain.CurrentDomain.FriendlyName} -w WordsFile -b BoringWordsFile -i ResultImageName " +
            "[-m FontSizeMultiplier] [-c WordsColor] [-g BackgroundColor] [-f FontFamily]\n";

        private static readonly Dictionary<string, Brush> BrushesByName = new Dictionary<string, Brush>
        {
            ["black"] = Brushes.Black,
            ["red"] = Brushes.Red,
            ["white"] = Brushes.White,
            ["green"] = Brushes.Green,
            ["blue"] = Brushes.Blue,
            ["yellow"] = Brushes.Yellow,
            ["brown"] = Brushes.Brown,
            ["gray"] = Brushes.Gray,
            ["orange"] = Brushes.Orange,
            ["pink"] = Brushes.Pink,
            ["cyan"] = Brushes.Cyan
        };

        public static void Main(string[] args)
        {
            if (!TryGetArguments(args, out var arguments))
                return;

            var builder = new ContainerBuilder();
            SetUpContainer(builder);
            var container = builder.Build();

            var image = container.Resolve<TagCloudGenerator>().Generate(arguments);
            image.Save(arguments.ImageFileName);       
        }

        private static bool TryGetArguments(string[] args, out Arguments arguments)
        {
            var newArguments = new Arguments();

            var parser = new FluentCommandLineParser();
            parser.Setup<string>('w').Callback(file => newArguments.WordsFileName = file).Required();
            parser.Setup<string>('b').Callback(boring => newArguments.BoringWordsFileName = boring).Required();
            parser.Setup<string>('i').Callback(name => newArguments.ImageFileName = name).Required();
            parser.Setup<string>('c').Callback(color => newArguments.WordsBrush = BrushesByName[color]);
            parser.Setup<string>('g').Callback(color => newArguments.BackgroundBrush = BrushesByName[color]);
            parser.Setup<string>('f').Callback(font => newArguments.FontFamily = new FontFamily(font));
            parser.Setup<int>('m').Callback(size => newArguments.Multiplier = size);

            if (CheckArgs(args, parser))
            {
                arguments = newArguments;
                return true;
            }

            arguments = null;
            return false;
        }

        private static bool CheckArgs(string[] args, FluentCommandLineParser parser)
        {
            try
            {
                var result = parser.Parse(args);
                if (result.HasErrors)
                {
                    Console.WriteLine("Wrong syntax\n");
                    Console.WriteLine(Help);
                    return false;
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Unknown color");
                Console.WriteLine("List of PossibleColors:");
                foreach (var color in BrushesByName.Keys)
                    Console.WriteLine($"\t{color}");
                return false;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Unknown Font");
                return false;
            }
            return true;
        }

        public static void SetUpContainer(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register(c => new Point()).As<Point>();
            builder.Register(c =>  new SpiralPointsGenerator(1, 0.01)).As<IPointsGenerator>();
        }
    }
}