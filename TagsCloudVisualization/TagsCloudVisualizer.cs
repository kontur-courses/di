using Autofac;

namespace TagsCloudVisualization
{
    public class TagsCloudVisualizer
    {
        public static void Main(string[] args)
        {
            var container = new ContainerBuilder();
            container.RegisterType<ISaver>().As<PngSaver>();
            container.RegisterType<ICloudLayout>().As<CircularCloudLayouter>();
            container.RegisterType<IWordProvider>().As<TxtWordProvider>();
            container.RegisterType<IWordsHandler>().As<WordsHandler>();
            container.RegisterType<IPointProvider>().As<PointProvider>();}
    }
}