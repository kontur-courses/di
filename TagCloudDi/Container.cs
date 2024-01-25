using System.Drawing;
using Autofac;
using TagCloudDi.Applications;
using TagCloudDi.Layouter;
using TagCloudDi.TextProcessing;
using IContainer = Autofac.IContainer;
using TextReader = TagCloudDi.TextProcessing.TextReader;

namespace TagCloudDi
{
    public static class Container
    {
        public static IContainer SetupContainer(Settings settings)
        {
            var builder = new ContainerBuilder();
            builder.Register(c => settings);
            builder.Register(c =>
                new Point(c.Resolve<Settings>().ImageWidth / 2, c.Resolve<Settings>().ImageHeight / 2)
            );
            builder.RegisterType<ArchimedeanSpiral>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().AsSelf();
            builder.RegisterType<TextReader>().AsSelf();
            builder.RegisterType<TextProcessor>().AsSelf();
            builder.RegisterType<RectangleGenerator>().AsSelf();
            builder.RegisterType<Drawer>().AsSelf();
            builder.RegisterType<ConsoleApplication>().As<IApplication>();

            return builder.Build();
        }
    }
}
