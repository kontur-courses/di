using System;
using CloudTagContainer.CUI;
using CloudTagContainer.ImageSavers;
using Ninject;

namespace CloudTagContainer
{
    public class Program
    {
        public static void Main()
        {
            var container = new StandardKernel();

            container.Bind<ConsoleInterface>().ToSelf();
            container.Bind<IWordsPreprocessor>().To<ToLowerPreprocessor>();
            container.Bind<IImageSaver>().To<PngSaver>();
            Console.Write("HELLO");
        }
    }
}