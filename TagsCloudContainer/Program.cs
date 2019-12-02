using System;
using Autofac;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BasicWordSelector>().As<IWordSelector>();
            builder.RegisterType<WordReader>().As<IWordReader>();
            builder.RegisterType<WordsTransformer>().As<IWordsTransformer>();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<ArgumentParser>().As<IArgumentParser>();
            builder.Register(c => c.Resolve<IArgumentParser>().ParseArgument(args)).As<Setting>();
            builder.RegisterType<Compositor>().As<Compositor>();
            var container = builder.Build();
            var compositor = container.Resolve<Compositor>();
            compositor.Composite();
        }
    }
}