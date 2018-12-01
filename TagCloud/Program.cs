using System;
using System.Drawing;
using Fclp;

namespace TagCloud
{
    public class Program
    {
        private static readonly string Help =
            "Program to generate circular layout of rectangles\n" +
            $"USAGE: {AppDomain.CurrentDomain.FriendlyName} -f File -i ResultImageName\n" +
            "where File lines have format:\n" +
            "WIDTH HEIGHT";

        public static void Main(string[] args)
        {
            var fileName = string.Empty;
            var imageFileName = string.Empty;

            var parser = new FluentCommandLineParser();
            parser.Setup<string>('f').Callback(file => fileName = file).Required();
            parser.Setup<string>('i').Callback(name => imageFileName = name).Required();

            var result = parser.Parse(args);
            if (result.HasErrors)
            {
                Console.WriteLine("Wrong syntax\n");
                Console.WriteLine(Help);
                return;
            }

            var center = new Point(0, 0);
            var generator = new SpiralPointsGenerator(1, 0.01);
            var layouter = new CircularCloudLayouter(center, generator);
            var reader = new SizeFileReader();
            var drawer = new RectanglesDrawer();
            try
            {
                new RectanglesImageGenerator(layouter, reader, drawer).Generate(fileName, imageFileName);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Wrong file syntax\n");
                Console.WriteLine(Help);
            }           
        }
    }
}