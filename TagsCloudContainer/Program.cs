using System;
using Autofac;
using CommandLine;
using TagsCloudContainer.FileOpeners;
using TagsCloudContainer.FileSavers;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.WordsColoringAlgorithms;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var parsedArguments = Parser.Default.ParseArguments<ConsoleUi>(args).Value;
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleUi>().AsSelf();
            builder.RegisterType<TxtOpener>().As<IFileOpener>();
            builder.RegisterType<PngSaver>().As<IFileSaver>();
            builder.RegisterType<InputFileHandler>().AsSelf();
            builder.RegisterType<BoringWordsDeleter>().AsSelf();
            builder.RegisterType<DefaultStainer>().As<IWordStainer>();
            builder.RegisterType<Spiral>().AsSelf();
            builder.RegisterInstance(new CustomSettings(parsedArguments.FontName,
                parsedArguments.BackGroungColor,
                parsedArguments.BrushColor,
                parsedArguments.PathToSave,
                parsedArguments.PathToOpen,
                parsedArguments.CanvasWidth,
                parsedArguments.CanvasHeight,
                parsedArguments.CanvasBorder,
                parsedArguments.RadiusOffset,
                parsedArguments.AngleOffset)).As<CustomSettings>();

            builder.RegisterType<BoringWordsDeleter>().AsSelf();
            builder.RegisterType<CoefficientCalculator>().AsSelf();
            builder.RegisterType<Spiral>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouterAlgorithm>();
            builder.RegisterType<CircularCloudDrawer>().AsSelf();
            var container = builder.Build();
            container.Resolve<CircularCloudDrawer>().DrawWords();
        }
    }
}