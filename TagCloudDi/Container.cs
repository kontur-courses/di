using System.Drawing;
using Autofac;
using TagCloudDi.Applications;
using TagCloudDi.Drawer;
using TagCloudDi.Layouter;
using TagCloudDi.TextProcessing;
using IContainer = Autofac.IContainer;

namespace TagCloudDi
{
    public static class Container
    {
        public static IContainer SetupContainer(Settings settings)
        {
            var builder = new ContainerBuilder();
            builder.Register(c => settings);
            builder.Register(c =>
                    new ArchimedeanSpiral(new Point(
                            c.Resolve<Settings>().ImageWidth / 2,
                            c.Resolve<Settings>().ImageHeight / 2),
                        c.Resolve<Settings>()))
                .As<IPointGenerator>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<FileTextReader>().As<ITextReader>();
            builder.RegisterType<TextProcessor>().As<ITextProcessor>();
            builder.RegisterType<RectanglesGenerator>().As<IRectanglesGenerator>();
            builder.RegisterType<Drawer.Drawer>().As<IDrawer>();
            builder.RegisterType<ConsoleApplication>().As<IApplication>();

            return builder.Build();
        }
    }
}
