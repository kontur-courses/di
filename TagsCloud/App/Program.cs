using System;
using System.Drawing;
using System.IO;
using Autofac;
using TagsCloud.App.Commands;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleClient>().As<IClient>();
            builder.RegisterType<SaveCommand>().As<ICommand>();
            builder.RegisterType<SetColorCommand>().As<ICommand>();
            builder.RegisterType<SetFontCommand>().As<ICommand>();
            builder.RegisterType<SetImageSizeCommand>().As<ICommand>();
            builder.RegisterType<TagCloudCommand>().As<ICommand>();
            builder.RegisterInstance(Console.Out).As<TextWriter>();
            builder.RegisterType<ImageSettings>().AsSelf()
                .As<IFontFamilyProvider, IImageColorProvider, IImageSizeProvider>()
                .SingleInstance();
            builder.RegisterType<TagCloudPainter>();
            builder.RegisterType<ImageHolder>().SingleInstance();
            builder.RegisterType<WordFrequency>();
            builder.RegisterType<TagCloudLayouter>();
            builder.RegisterType<TagCloudPainter>();
            builder.RegisterType<WordChecker>();
            builder.RegisterType<ImageSize>();
            builder.RegisterInstance(new FontFamily("Arial")).As<FontFamily>();
            builder.RegisterType<SpiralAlgorithm>().As<ILayoutAlgorithm>();
            var container = builder.Build();
            container.Resolve<IClient>().Run();
        }
    }
}