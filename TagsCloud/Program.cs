using System.Drawing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Visualizer;
using Point = TagsCloudVisualization.Layouter.Point;
using Size = System.Drawing.Size;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Point>().ImplementedBy<Point>()
                .DependsOn(Dependency.OnValue("x", 0)).DependsOn(Dependency.OnValue("y", 0)));
            container.Register(Component.For<IFileReader>().ImplementedBy<FileReader>()
                .DependsOn(Dependency.OnValue("path", @"D:\input.txt")));
            container.Register(Component.For<ICloudLayouter>().ImplementedBy<CircularCloudLayouter>());
            container.Register(Component.For<IVisualizer>().ImplementedBy<VisualizerToFile>()
                .DependsOn(Dependency.OnValue("outputPath", @"D:\output.png"))
                .DependsOn(Dependency.OnValue("backgroundColor", Color.Beige))
                .DependsOn(Dependency.OnValue("imageSize", new Size(10000, 10000))));
            container.Register(Component.For<IColorScheme>().ImplementedBy<RandomColorScheme>());
            container.Register(Component.For<IFontScheme>().ImplementedBy<ArialFontScheme>());
            container.Register(Component.For<ISizeScheme>().ImplementedBy<LinearSizeScheme>());
            container.Register(Component.For<IWordExcluder>().ImplementedBy<WordExcluderByFile>()
                .DependsOn(Dependency.OnValue("path", @"D:\stopwords.txt")));
            container.Register(Component.For<IStatisticsCollector>().ImplementedBy<StatisticsCollector>());
            container.Register(Component.For<TagsCloud>().ImplementedBy<TagsCloud>());
            var tagsCloud = container.Resolve<TagsCloud>();
            tagsCloud.Generate(@"D:\input.txt");
        }
    }
}