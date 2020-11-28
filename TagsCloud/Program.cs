using System;
using System.Drawing;
using Autofac;

namespace TagsCloud
{
    public static class Program
    {
        public static void Main()
        {
            while (true)
            {
                MakeCloud();
            }
        }

        public static void MakeCloud()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WordLayouter>();
            builder.RegisterType<CloudRenderer>();
            
            Console.Write("File path: ");
            var filePath = Console.ReadLine();
            var words = filePath.ReadWords();
            if (words == null) return;
            
            var width = ReadInteger("Image width");
            var height = ReadInteger("Image height");

            var font = ReadFont();
            builder.RegisterInstance(font);
            
            var statistic = words.GetWordStatistics();
            var container = builder.Build();
            
            var layouter = container.Resolve<WordLayouter>();
            layouter.AddWords(statistic);
            var cloudRenderer = container.Resolve<CloudRenderer>();
            var path = cloudRenderer.RenderCloud(layouter, width, height);
            Console.WriteLine($"Cloud saved in {path}");
        }

        private static int ReadInteger(string parameterName)
        {
            while (true)
            {
                Console.Write($"{parameterName}: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var result)) return result;
                Console.WriteLine("Incorrect string");
            }
        }
        
        private static FontFamily ReadFont()
        {
            while (true)
            {
                Console.Write("Font: ");
                var input = Console.ReadLine();
                try
                {
                    var font = new FontFamily(input);
                    return font;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"{input} doesn't exist");
                }
            }
        }
    }
}