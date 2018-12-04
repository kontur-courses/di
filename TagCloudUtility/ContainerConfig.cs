using System.Drawing;
using Autofac;
using TagCloud.Layouter;
using TagCloud.PointsSequence;
using TagCloud.RectanglePlacer;
using TagCloud.Utility.Data;
using TagCloud.Utility.Models.Tag;
using TagCloud.Utility.Models.TextReader;
using TagCloud.Utility.Models.WordFilter;
using TagCloud.Visualizer;
using TagCloud.Visualizer.Settings;

namespace TagCloud.Utility
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<Logger>()
                .As<ILogger>();

            builder
                .RegisterType<TxtReader>()
                .As<ITextReader>()
                .Named<ITextReader>("txt");

            builder
                .RegisterType<RusFilter>()
                .As<IWordFilter>();

            builder
                .RegisterType<Spiral>()
                .As<IPointsSequence>();

            builder
                .RegisterType<CenterRectanglePlacer>()
                .As<IRectanglePlacer>();

            builder
                .RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>()
                .WithParameter(new TypedParameter(typeof(IPointsSequence), "pointsSequence"))
                .WithParameter(new TypedParameter(typeof(IRectanglePlacer), "rectanglePlacer"));

            builder
                .RegisterType<CloudVisualizer>()
                .As<ICloudVisualizer>()
                .WithParameter(new TypedParameter(typeof(IDrawSettings), "settings"));

            builder
                .RegisterType<DrawSettings>()
                .As<IDrawSettings>()
                .WithParameter(new TypedParameter(typeof(DrawFormat), "drawFormat"))
                .WithParameter(new TypedParameter(typeof(Brush), "brush"))
                .WithParameter(new TypedParameter(typeof(Font), "font"));

            builder
                .RegisterType<TagReader>()
                .As<ITagReader>()
                .WithParameter(new TypedParameter(typeof(TagContainer), "tagGroups"));

            builder
                .RegisterInstance(
                    new TagContainer
                    {
                        {"Big", new FrequencyGroup(0.9, 1), new Size(80, 150)},
                        {"Average", new FrequencyGroup(0.6, 0.9), new Size(60, 100)},
                        {"Small", new FrequencyGroup(0, 0.6), new Size(30, 50)}
                    })
                .AsSelf();

            return builder.Build();
        }
    }
}
