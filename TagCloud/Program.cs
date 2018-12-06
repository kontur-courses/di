using System.Drawing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagCloud.Enums;
using TagCloud.Interfaces;
using TagCloud.Layouter;
using TagCloud.Visualizer;
using Point = TagCloud.Layouter.Point;
using Size = System.Drawing.Size;

namespace TagCloud
{
    public class Program
    {
        public static ColorScheme ColorScheme = ColorScheme.RandomColors;
        public static CloudLayouterType CloudLayouterType = CloudLayouterType.ArithmeticSpiral;
        public static FontScheme FontScheme = FontScheme.Arial;
        public static SizeScheme SizeScheme = SizeScheme.Linear;

        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Point>().ImplementedBy<Point>()
                .DependsOn(Dependency.OnValue("x", 0)).DependsOn(Dependency.OnValue("y", 0)));
            container.Register(Component.For<IFileReader>().ImplementedBy<FileReader>()
                .DependsOn(Dependency.OnValue("path", @"D:\input.txt")));
            if (CloudLayouterType == CloudLayouterType.ArithmeticSpiral)
                container.Register(Component.For<ICloudLayouter>().ImplementedBy<CircularCloudLayouter>());
            container.Register(Component.For<IVisualizer>().ImplementedBy<VisualizerToFile>()
                .DependsOn(Dependency.OnValue("outputPath", @"D:\output.png"))
                .DependsOn(Dependency.OnValue("backgroundColor", Color.Beige))
                .DependsOn(Dependency.OnValue("imageSize", new Size(10000, 10000))));
            if (ColorScheme == ColorScheme.RandomColors)
                container.Register(Component.For<IColorScheme>().ImplementedBy<RandomColorScheme>());
            if (FontScheme == FontScheme.Arial)
                container.Register(Component.For<IFontScheme>().ImplementedBy<ArialFontScheme>());
            if (SizeScheme == SizeScheme.Linear)
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