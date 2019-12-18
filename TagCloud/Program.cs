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
            containerBuilder.RegisterType<TxtTextRider>().As<IFileTextRider>().SingleInstance();
            containerBuilder.RegisterType<MSWordTextRider>().As<IFileTextRider>().SingleInstance();
            
            containerBuilder.RegisterInstance(PainterConfig.Default()).As<PainterConfig>().SingleInstance();
            containerBuilder.RegisterType<Painter>().As<ITagCloudPainter>().SingleInstance();
            
            containerBuilder.RegisterType<FrequencyTextAnalyzer>().As<ITextAnalyzer>().SingleInstance();

            containerBuilder.RegisterInstance(new LayouterFactory()).AsSelf().SingleInstance();
            containerBuilder.RegisterType<ArchimedeanSpiral>().As<ISpiral>().SingleInstance();
            containerBuilder.RegisterType<RectangularSpiral>().As<ISpiral>().SingleInstance();
            
            containerBuilder.RegisterType<DrawTagCloud>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<ImageName>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<ImageSize>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<MaxFontSize>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<MinFontSize>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<PathToFile>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<PathToSave>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<AddSkipWord>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<ListLayoutAlgorithms>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<LayoutAlgorithm>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<NewPalette>().As<IConsoleCommand>().SingleInstance();
            containerBuilder.RegisterType<AddColor>().As<IConsoleCommand>().SingleInstance();
            
            return containerBuilder.Build();
        }
    }
}