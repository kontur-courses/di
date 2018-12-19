using System;
using System.Drawing;
using System.IO;
using TagsCloudContainer.CircularCloudLayouter;
using Autofac;


namespace TagsCloudContainer
{
    internal class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Want to continue?(Y/N): ");
                var line = Console.ReadLine();
                if (line == "N" || line == "n" || line.ToLower() == "exit")
                    break;
                if (line == "Y" || line == "y")
                {
                    ReadData();
                    Console.WriteLine("Success!\n");
                }
                else
                    Console.WriteLine("Unknown command. Please try again");
            }
        }

        private static void ReadData()
        {
            Console.Write("Input text file name: ");
            var filePath = Console.ReadLine();
            Console.Write("Input angle for customizing creating tag cloud[unnecessary parameter]: ");
            var angleForDirectionStr = Console.ReadLine();

            int angle;
            try
            {
                angle = int.Parse(angleForDirectionStr);
            }
            catch (Exception e)
            {
                angle = 1;
            }

            CreateTagCloudImage(
                string.IsNullOrEmpty(filePath) ? @"..\..\tmpTextFile.txt" : filePath,
                angle);
        }

        private static void CreateTagCloudImage(string filePath, double angleForDirection = 1)
        {
            var words = File.ReadAllLines(filePath);
            Func<Word, Size> wordSizeFunc = w => new Size(w.Count * 20 * w.Value.Length, w.Count * 20);
            var t = GetFileName(filePath);

            var builder = new ContainerBuilder();
            builder.RegisterType<Drawer>().As<IDrawer<Word>>();
            builder.RegisterType<ItemToDraw<Rectangle>>().As<IItemToDraw<Rectangle>>();
            builder.RegisterType<Word>().As<IWord>();
            builder.RegisterType<WordStorage>().As<IWordStorage>().WithParameter("wordsToHandle", words);
            builder.RegisterType<WordsCustomizer>().AsSelf().SingleInstance();
            builder.RegisterType<RectangleStorage>().AsSelf().SingleInstance();
            builder.RegisterType<Direction>().As<IDirection<double>>().WithParameter("angleShift", angleForDirection);
            builder.RegisterType<WordLayouter>().WithParameter("getWordSize", wordSizeFunc).AsSelf().SingleInstance();
            builder.RegisterType<DrawSettings<Word>>().WithParameter("filePath", GetFileName(filePath));
            builder.RegisterType<CircularCloudLayout>().As<IRectangleLayout>();
            builder.Register(p => new Point()).As<Point>();

            Container = builder.Build();

            var drawer = Container.Resolve<IDrawer<Word>>();
            drawer.DrawItems();

            Container.Dispose();
        }

        private static string GetFileName(string fullPath)
        {
            return fullPath.Substring(0, fullPath.Length - 4);
        }
    }
}