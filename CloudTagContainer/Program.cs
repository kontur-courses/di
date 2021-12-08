using System;
using System.Drawing;
using CloudTagContainer.ImageSavers;
using CUI;
using Ninject;

namespace CloudTagContainer
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new StandardKernel();

            var setting = new VisualizerSettings(
                new Size(1920, 1080),
                new Font("Arial", 24, FontStyle.Bold),
                Color.Blue,
                Color.Chocolate
            );

            container.Bind<ConsoleInterface>().ToSelf();
            container.Bind<IWordsPreprocessor>().To<ToLowerPreprocessor>();
            container.Bind<IImageSaver>().To<PngSaver>();
            container.Bind<ILayouter>().To<CircularCloudLayouter>();
            container.Bind<ISpiral>().To<ExpandingSquare>();
            container.Bind<IWordSizer>().To<CountingWordSizer>();
            container.Bind<IFileStreamFactory>().To<FileStreamFactory>();
            container.Bind<VisualizerSettings>().ToConstant(setting);

            var cui = container.Get<ConsoleInterface>();
            cui.Run(new Options() {InputTextPath = "input.txt", PathToSaveImage = "result.png"});
            Console.Write("HELLO");
        }
    }
}