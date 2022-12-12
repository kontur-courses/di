using System;
using Autofac;
using CommandLine;
using TagsCloudContainer.FileOpeners;
using TagsCloudContainer.FileSavers;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.UI;
using TagsCloudContainer.WordsColoringAlgorithms;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var app = new ConsoleUi();

            var parsedArguments = app.GetType() == new ConsoleUi().GetType()
                ? Parser.Default.ParseArguments<ConsoleUi>(args).Value
                : app;
            CheckArguments(parsedArguments);
            RegisterDependencies(builder, parsedArguments);
            var container = builder.Build();
            var frequencyDictionary = container.Resolve<InputFileHandler>()
                .FormFrequencyDictionary(parsedArguments.PathToOpen);
            var colorSequence = container.Resolve<IWordStainer>()
                .GetColorsSequence(frequencyDictionary.Count, parsedArguments.BrushColor);
            var coefficient = ScaleCoefficientCalculator.CalculateScaleCoefficient(parsedArguments.CanvasWidth,
                parsedArguments.CanvasHeight, parsedArguments.CanvasBorder);
            CircularCloudDrawer.DrawWords(colorSequence,
                frequencyDictionary,
                container.Resolve<IFileSaver>(),
                parsedArguments,
                coefficient,
                container.Resolve<ICloudLayouterAlgorithm>());
        }

        private static void CheckArguments(IUi parsedArguments)
        {
            if (parsedArguments.CanvasBorder < 0)
                throw new ArgumentException("Borders can't be less than zero");
            if (parsedArguments.CanvasHeight < parsedArguments.CanvasBorder * 2)
                throw new ArgumentException("Too small canvas height");
            if (parsedArguments.CanvasWidth < parsedArguments.CanvasBorder * 2)
                throw new ArgumentException("Too small canvas width");
            if (parsedArguments.AngleOffset > 1 || parsedArguments.AngleOffset < 0)
                throw new ArgumentException("Invalid angle offset");
            if (parsedArguments.RadiusOffset > 1 || parsedArguments.RadiusOffset < 0)
                throw new ArgumentException("Invalid radius offset");
        }


        private static void RegisterDependencies(ContainerBuilder builder, IUi parsedArguments)
        {
            RegisterFileReaderDependency(builder, parsedArguments.PathToOpen);
            builder.RegisterInstance(parsedArguments).As<IUi>();
            builder.RegisterType<PngSaver>().As<IFileSaver>();
            builder.RegisterType<InputFileHandler>().AsSelf();
            builder.RegisterType<DefaultWordPainter>().As<IWordStainer>();
            builder.RegisterType<Spiral>().AsSelf();
            builder.RegisterType<Spiral>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouterAlgorithm>();
        }

        private static void RegisterFileReaderDependency(ContainerBuilder builder, string pathToOpen)
        {
            var index = pathToOpen.LastIndexOf('.');
            var format = pathToOpen.Substring(index + 1);
            switch (format)
            {
                case "txt":
                    builder.RegisterType<TxtReader>().As<IFileReader>();
                    break;
                default:
                    throw new ArgumentException("Unknown file format");
            }
        }
    }
}