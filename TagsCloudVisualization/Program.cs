using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagsCloudVisualization.Layouters.CloudLayouters;
using TagsCloudVisualization.Layouters.Spirals;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Text.TextReaders;
using TextReader = TagsCloudVisualization.Text.TextReader;

namespace TagsCloudVisualization
{
    public static class Program
    {
        static void Main()
        {
            var container = new WindsorContainer();

            //TODO: Init stream

            container.Register(Component.For<TextReader>().ImplementedBy<TxtReader>());

            //TODO: Add some preprocess actions
            //container.Register(Component.For<IPreprocessAction>().ImplementedBy<PreprocessAction>());
            //container.Register(Component.For<IPreprocessAction>().ImplementedBy<PreprocessAction>());
            //container.Register(Component.For<IPreprocessAction>().ImplementedBy<PreprocessAction>());

            container.Register(Component.For<WordLayoutPainter>().ImplementedBy<DefaultWordLayoutPainter>());

            container.Register(Component.For<ISpiral>().ImplementedBy<ArchimedesSpiral>());
            container.Register(Component.For<ICloudLayouter>().ImplementedBy<CircularCloudLayouter>());

            //TODO: Add some visualizer actions
            //container.Register(Component.For<IVisualizerAction>().ImplementedBy<VisualizerAction>());
            //container.Register(Component.For<IVisualizerAction>().ImplementedBy<VisualizerAction>()); Some actions
            //container.Register(Component.For<IVisualizerAction>().ImplementedBy<VisualizerAction>());

            //TODO: Start interface
        }
    }
}