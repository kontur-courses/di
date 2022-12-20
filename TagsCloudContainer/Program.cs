using System;
using Autofac;
using CommandLine;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.FileSavers;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.UI;
using TagsCloudContainer.WordsColoringAlgorithms;

namespace TagsCloudContainer
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var app = new ConsoleUiSettings();

            var parsedArguments = app.GetType() == new ConsoleUiSettings().GetType()
                ? Parser.Default.ParseArguments<ConsoleUiSettings>(args).Value
                : app;
            if (parsedArguments is null)
                return;
            try
            {
                CheckArguments(parsedArguments);
                RegisterDependencies(builder, parsedArguments);
                var container = builder.Build();
                CircularCloudDrawer.DrawWords(container.Resolve<WordsColoringFactory>(),
                    container.Resolve<FileSaverFactory>(),
                    container.Resolve<FileReaderFactory>(),
                    parsedArguments,
                    container.Resolve<LayouterFactory>());
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
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
            builder.RegisterInstance(new FileSaverFactory(() => parsedArguments)).As<FileSaverFactory>();
            builder.RegisterInstance(new FileReaderFactory(() => parsedArguments)).As<FileReaderFactory>();
            builder.RegisterInstance(new WordsColoringFactory(() => parsedArguments)).As<WordsColoringFactory>();
            builder.RegisterInstance(new LayouterFactory(() => parsedArguments)).As<LayouterFactory>();
            builder.RegisterInstance(parsedArguments).As<IUi>();
            builder.RegisterType<Spiral>().AsSelf();
        }
    }
}