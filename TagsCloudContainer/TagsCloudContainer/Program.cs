using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CircularCloudLayouter;
using Autofac;

namespace TagsCloudContainer
{
    internal class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            while (true)
            {
                Console.Write(@"Want to continue?(Y/N): ");
                var line = Console.ReadLine().ToLower();
                if (line == "n" || line == "exit")
                    break;
                if (line == "y")
                {
                    ReadData();
                    Console.WriteLine("Success!\n");
                }
                else
                    Console.WriteLine(@"Unknown command. Please try again");
            }
        }

        private static void ReadData()
        {
            var filePath = ReadFilePath();
            var angleForDirectionStr = ReadAngleForDirection();
            var wordsToIgnore = ReadWordsToIgnore();

            int angle;
            if (!int.TryParse(angleForDirectionStr, out angle))
                angle = 1;

            CreateTagCloudImage(
                string.IsNullOrEmpty(filePath) ? @"..\..\tmpTextFile.txt" : filePath,
                angle, w => new Size(w.Count * 20 * w.Value.Length, w.Count * 20), wordsToIgnore);
        }

        private static void CreateTagCloudImage(string filePath, double angleForDirection,
            Func<Word, Size> wordSizeFunc, List<string> wordsToIgnore)
        {
            using (Container = ContainerBuilder(filePath, angleForDirection, wordSizeFunc, wordsToIgnore).Build())
            {
                var drawer = Container.Resolve<IDrawer>();
                drawer.DrawItems();
            }
        }

        private static ContainerBuilder ContainerBuilder(string filePath, double angleForDirection,
            Func<Word, Size> wordSizeFunc, List<string> wordsToIgnore)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Drawer>().As<IDrawer>();
            builder.RegisterType<WordStorage>().As<IWordStorage>();
            builder.RegisterType<RectangleStorage>().As<IRectangleStorage>().SingleInstance();
            builder.RegisterType<CircularCloudLayout>().As<IRectangleLayout>();

            builder.RegisterType<Reader>().As<IReader>()
                .WithParameter("filePath", filePath);
            builder.RegisterType<WordsCustomizer>().As<IWordsCustomizer>().SingleInstance()
                .WithParameter("wordsToIgnore", wordsToIgnore);
            builder.RegisterType<WordLayouter>().As<ILayouter<Word>>()
                .WithParameter("getWordSize", wordSizeFunc).AsSelf().SingleInstance();
            builder.RegisterType<Direction>().As<IDirection<double>>()
                .WithParameter("angleShift", angleForDirection);
            builder.RegisterType<DrawSettings<Word>>().As<IDrawSettings<Word>>()
                .WithParameter("filePath", GetFileName(filePath));
            return builder;
        }

        private static string GetFileName(string fullPath)
        {
            return fullPath.Substring(0, fullPath.Length - 4);
        }

        private static string ReadFilePath()
        {
            Console.Write(@"Input text file name: ");
            return Console.ReadLine();
        }

        private static string ReadAngleForDirection()
        {
            Console.Write(@"Input angle for customizing tag cloud creating[unnecessary parameter]: ");
            var angleForDirectionStr = Console.ReadLine();
            return angleForDirectionStr;
        }

        private static List<string> ReadWordsToIgnore()
        {
            Console.Write(@"Specify path to the file with words to ignore[unnecessary parameter]: ");
            var filePath = Console.ReadLine();
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    return null;

                var wordsToIgnore = new Reader(filePath)
                    .ReadWords()
                    .Where(w => !string.IsNullOrEmpty(w))
                    .Distinct()
                    .ToList();

                return wordsToIgnore.Count == 0 ? null : wordsToIgnore;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}