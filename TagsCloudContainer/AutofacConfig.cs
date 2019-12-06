using System.Drawing;
using Autofac;
using TagsCloudContainer.TagCloudVisualization;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer(string font)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.RegisterType<LayoutVisualization>().AsSelf()
                .WithParameter("baseFont", new Font(font, 10))
                .WithParameter("pen", new Pen(Color.Black));

            builder.RegisterType<WordProcessor>().AsSelf();
            builder.RegisterType<MyStem>().AsSelf();
            builder.RegisterType<Spiral>().AsSelf();

            return builder.Build();
        }
    }
}