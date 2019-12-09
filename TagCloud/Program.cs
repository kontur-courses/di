using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using System;
using System.IO;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using TagCloud.Actions;
using TagCloud.IServices;

namespace TagCloud
{
    public class Program
    {
        public static void Main()
        {
            var container = GetContainer();
            var visaulization = container.Resolve<ICloudVisualization>();
            var client = container.Resolve<IClient>();
            client.Start(visaulization);
        }

        private static WindsorContainer GetContainer()
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));
            container.Register(Component.For<IWordsToTagsParser>().ImplementedBy<WordsToTagsParser>());
            container.Register(Component.For<IWordsHandler>().ImplementedBy<WordsHandler>());
            container.Register(Component.For<ITagCollectionFactory>().ImplementedBy<TagCollectionFactory>()
                .LifestyleSingleton());
            container.Register(Component.For<IAlgorithm>().ImplementedBy<ArchimedianSprial>());
            container.Register(Component.For<ICircularCloudLayouter>().ImplementedBy<CircularCloudLayouter>());
            container.Register(Component.For<ICloud>().ImplementedBy<Cloud>());
            container.Register(Component.For<ICloudVisualization>().ImplementedBy<CloudVisualization>());
            container.Register(Component.For<IAction>().ImplementedBy<NewImageAction>().LifestyleSingleton());
            container.Register(Component.For<IAction>().ImplementedBy<SaveImageAction>().LifestyleSingleton());
            container.Register(Component.For<IAction>().ImplementedBy<ShowImageAction>().LifestyleSingleton());
            container.Register(Component.For<IAction>().ImplementedBy<ExitAction>().LifestyleSingleton());
            container.Register(Component.For<IClient>().ImplementedBy<Client>());
            return container;
        }
    }
}
