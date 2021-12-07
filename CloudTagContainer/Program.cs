using System;
using Ninject;

namespace CloudTagContainer
{
    public class Program
    {
        public static void Main()
        {
            var container = new StandardKernel();

            container.Bind<IFileReader>().To<FileReader>().InSingletonScope();
            container.Bind<ILayouter>().To<CircularCloudLayouter>().InSingletonScope();
            container.Bind<ISpiral>().To<ExpandingSquare>().InSingletonScope();
            container.Bind<IWordsPreprocessor>().To<ToLowerPreprocessor>().InSingletonScope();
            container.Bind<Visualizer>().ToSelf();
            
            Console.Write("HELLO");
        }
    }
}