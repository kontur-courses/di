using Autofac;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public class Program
    {
        private static void Main(string[] args)
        {
            CompositionRoot().Resolve<TagsCloudApp>().Run(args);
        }

        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CloudParametersParser>().As<ICloudParametersParser>();
            builder.RegisterType<WordDataProvider>().As<IWordDataProvider>();
            builder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>()
                .WithParameter(new TypedParameter(typeof(IPointGenerator), "pointGenerator"));
            builder.RegisterType<Spiral>().Named<IPointGenerator>("spiral");
            builder.RegisterType<Heart>().Named<IPointGenerator>("heart");
            builder.RegisterType<Astroid>().Named<IPointGenerator>("astroid");
            builder.RegisterType<WordsExtractor>().As<IWordsExtractor>();
            return builder.Build();
        }
    }
}