using System;
using System.Drawing;
using System.Linq;
using Autofac;
using TagsCloudContainer.Common;
using TagsCloudContainer.Layouters;
using TagsCloudContainer.Painting;
using TagsCloudContainer.Preprocessors;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Visualizators;

namespace TagsCloudContainer
{
    internal class Program
    {
        private const string textFilename = "..\\..\\..\\Tags\\startTags.txt";
        private const string imageFilename = "..\\..\\..\\images\\image1.jpg";

        private static void Main(string[] args)
        {
            var container = RegisterDependencies();
            ResolveContainer(container);
        }

        private static void ResolveContainer(IContainer container)
        {
            var minHeight = 12;
            var maxScale = 10;
            using (var scope = container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<TagReader>();
                var parser = scope.Resolve<WordsCountParser>();
                var preprocessor = scope.Resolve<TagsPreprocessor>();
                var layouter = scope.Resolve<TagLayouter>();
                var painter = scope.Resolve<TagPainter>();
                var visualizator = scope.Resolve<IVisualizator<ITag>>();
                var settings = scope.Resolve<IVisualizatorSettings>();

                var text = reader.Read(textFilename);
                var tags = parser.Parse(text);
                tags = preprocessor.Process(tags);
                var cloud = layouter.PlaceTagsInCloud(tags, minHeight, maxScale);
                painter.SetPalettes(cloud.Elements);
                visualizator.Visualize(settings, cloud);
            }
        }

        private static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            RegisterLibraryDependencies(builder);
            RegisterProjectDependencies(builder);
            RegisterPreprocessors(builder);
            return builder.Build();
        }

        private static void RegisterLibraryDependencies(ContainerBuilder builder)
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
                .As<IVisualizatorSettings>()
                .WithParameter("filename", imageFilename);
        }

        private static void RegisterProjectDependencies(ContainerBuilder builder)
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

        private static void RegisterPreprocessors(ContainerBuilder builder)
        {
            var preprocessor = typeof(IPreprocessor);
            var preprocessors = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.FullName.Contains("TagsCloudContainer"))
                .GetTypes()
                .Where(t => preprocessor.IsAssignableFrom(t))
                .ToArray();
            builder.RegisterTypes(preprocessors).As<IPreprocessor>();
            builder.RegisterType<TagsPreprocessor>().AsSelf();
        }
    }
}
