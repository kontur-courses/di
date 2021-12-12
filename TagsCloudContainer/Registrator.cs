using System.Drawing;
using Autofac;
using TagsCloudContainer.Common;
using TagsCloudContainer.Layouters;
using TagsCloudContainer.Painting;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Visualizators;

namespace TagsCloudContainer
{
    internal class Registrator
    {
        private readonly ContainerBuilder builder;

        public Registrator(ContainerBuilder builder)
        {
            this.builder = builder;
        }

        public ContainerBuilder RegisterDependencies()
        {
            RegisterLibraryDependencies();
            RegisterProjectDependencies();
            PreprocessorsRegistrator.RegisterPreprocessors(builder);
            return builder;
        }

        private void RegisterLibraryDependencies()
        {
            builder.RegisterType<Cloud>()
                .As<ICloud<ITag>>()
                .WithParameter("center", new PointF());

            builder.RegisterType<Spiral>()
                .As<ISpiral>()
                .WithParameter("center", new PointF());

            builder.RegisterType<Tag>()
                .As<ITag>();

            builder.RegisterType<Tag>();

            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();

            builder.RegisterType<TagsVisualizator>()
                .As<IVisualizator<ITag>>();

            builder.RegisterType<TagsVisualizatorSettings>()
                .As<IVisualizatorSettings>();
        }

        private void RegisterProjectDependencies()
        {
            builder.RegisterType<FileBlobStorage>()
                .As<IBlobStorage>();

            builder.RegisterType<Serializer>()
                .As<ISerializer>();

            builder.RegisterType<TagCircularLayouter>()
                .As<TagLayouter>();

            builder.RegisterType<PalettesMaker>()
                .As<IPalettesMaker>();

            builder.RegisterType<TagReader>().AsSelf();
            builder.RegisterType<TagPainter>().AsSelf();
            builder.RegisterType<WordsCountParser>().AsSelf();
        }
    }
}