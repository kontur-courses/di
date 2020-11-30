using System;
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
            builder.RegisterType<ImageHolder>();
            builder.RegisterType<WordFrequency>();
            builder.RegisterType<TagCloudLayouter>();
            builder.RegisterType<TagCloudPainter>();
            builder.RegisterType<WordChecker>();
            builder.RegisterType<SpiralAlgorithm>().As<ILayoutAlgorithm>();
            var container = builder.Build();
            container.Resolve<IClient>().Run();
        }
    }
}