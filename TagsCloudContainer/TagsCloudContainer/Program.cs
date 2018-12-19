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
            var filePath = @"..\..\tmpTextFile";
            var words = File.ReadAllLines(filePath + ".txt");
            Func<Word, Size> wordSizeFunc = w => new Size(w.Count * 20 * w.Value.Length, w.Count * 20);
             
            var builder = new ContainerBuilder();
            builder.RegisterType<Drawer>().As<IDrawer<Word>>();
            builder.RegisterType<ItemToDraw<Rectangle>>().As<IItemToDraw<Rectangle>>();
            builder.RegisterType<Word>().As<IWord>();
            builder.RegisterType<WordStorage>().As<IWordStorage>().WithParameter("wordsToHandle", words);
            builder.RegisterType<WordsCustomizer>().AsSelf().SingleInstance();
            builder.RegisterType<RectangleStorage>().AsSelf().SingleInstance();
            builder.RegisterType<Direction>().As<IDirection<double>>();
            builder.RegisterType<WordLayouter>().WithParameter("getWordSize", wordSizeFunc).AsSelf().SingleInstance();
            builder.RegisterType<DrawSettings<Word>>().WithParameter("filePath", filePath);
            builder.RegisterType<CircularCloudLayout>().As<IRectangleLayout>();
            builder.Register(p => new Point()).As<Point>();

            Container = builder.Build();

            var drawer = Container.Resolve<IDrawer<Word>>();
            drawer.DrawItems();

            Container.Dispose();
        }
    }
}