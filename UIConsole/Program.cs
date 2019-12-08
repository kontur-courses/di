using TagCloud;
using Autofac;

namespace UIConsole
{
    internal class Program
    {
        
        //Я пока не разобрался почему оно не работает, но разберусь))
        public static void Main()
        {
            var container = CreateContainer();
            var tagCloudPainter = container.Resolve<ITagCloudPainter>();
            tagCloudPainter.Draw();
        }

        private static IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(new DefaultTextRiderConfig()).As<ITextRiderConfig>();
            containerBuilder.RegisterInstance(new DefaultPainterConfig()).As<IPainterConfig>();
            containerBuilder.RegisterType<LineTextRider>().As<ITextRider>();
            containerBuilder.RegisterType<FrequencyTextAnalyzer>().As<ITextAnalyzer>();
            containerBuilder.RegisterType<TagCloudPainter>().As<ITagCloudPainter>();
            return containerBuilder.Build();
        }
    }
}