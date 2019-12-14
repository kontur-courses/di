using System.Drawing;
using Autofac;
using TagCloud.ConsoleCommands;
using TagCloudPainter;
using TagsCloudVisualization.Spirals;
using TagsCloudVisualization.TagCloudLayouter;
using TextPreprocessor.TextAnalyzers;
using TextPreprocessor.TextRiders;
using UIConsole;

namespace TagCloud
{
    internal static class Program
    {
        public static void Main()
        {
            var container = CreateContainer();
            var consoleUi = container.Resolve<ConsoleUserInterface>();
            consoleUi.Run();
        }

        private static IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ConsoleUserInterface>().AsSelf().SingleInstance();
            
            containerBuilder.RegisterInstance(TextRiderConfig.Default()).As<TextRiderConfig>().SingleInstance();
            containerBuilder.RegisterType<FileFileTextRider>().As<IFileTextRider>().SingleInstance();
            
            containerBuilder.RegisterType<FrequencyTextAnalyzer>().As<ITextAnalyzer>().SingleInstance();

            containerBuilder.RegisterInstance(new LayouterFactory()).AsSelf().SingleInstance();
            
            containerBuilder.RegisterInstance(PainterConfig.Default()).As<PainterConfig>().SingleInstance();
            containerBuilder.RegisterType<Painter>().As<ITagCloudPainter>().SingleInstance();

            containerBuilder.RegisterType<DrawDefaultTagCloud>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<SetImageName>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<SetImageSize>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<MaxFontSize>().As<IConsoleCommand>().SingleInstance();
            
            containerBuilder.Register(c => new Point(500, 500)).As<Point>();
            return containerBuilder.Build();
        }
    }
}