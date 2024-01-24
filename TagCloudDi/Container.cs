using System.Drawing;
using Autofac;
using TagCloudDi.Layouter;
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
                new Point(c.Resolve<Settings>().ImageWidth / 2, c.Resolve<Settings>().ImageHeight / 2)
            );
            builder.RegisterType<ArchimedeanSpiral>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().AsSelf();

            return builder.Build();
        }
    }
}
