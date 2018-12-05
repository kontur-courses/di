using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Visualizator;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Point>().ImplementedBy<Point>()
                .DependsOn(Dependency.OnValue("x", 0)).DependsOn(Dependency.OnValue("y", 0)));
            container.Register(Component.For<IFileReader>().ImplementedBy<FileReader>());
            container.Register(Component.For<ICloudLayouter>().ImplementedBy<CircularCloudLayouter>());
            container.Register(Component.For<IVisualizer>().ImplementedBy<VisualizerToFile>());
            container.Register(Component.For<TagsCloud>().ImplementedBy<TagsCloud>());
            var tagsCloud = container.Resolve<TagsCloud>();
        }
    }
}