using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using System;
using TagCloud.Actions;
using TagCloud.IServices;

namespace TagCloud
{
    public class Program
    {
        public static void Main()
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Register(Component.For<IWordsToTagsParser>().ImplementedBy<WordsToTagsParser>());
            container.Register(Component.For<IWordsHandler>().ImplementedBy<WordsHandler>());
            container.Register(Component.For<ITagsConfig>().ImplementedBy<TagsConfig>());
            container.Register(Component.For<IAlgorithm>().ImplementedBy<ArchimedianSprial>());
            container.Register(Component.For<ICircularCloudLayouter>().ImplementedBy<CircularCloudLayouter>());
            container.Register(Component.For<ICloud>().ImplementedBy<Cloud>());
            container.Register(Component.For<ICloudVisualization>().ImplementedBy<CloudVisualization>());
            container.Register(Component.For<IAction>().ImplementedBy<SaveImageAction>());
            container.Register(Component.For<IAction>().ImplementedBy<ShowImageAction>());
            container.Register(Component.For<IClientDataFactory>().ImplementedBy<ClientDataFactory>().LifestyleSingleton());
            var settings = new Settings(500, 500);
            settings.Load(container.Resolve<IClientDataFactory>());
            var Cloud = container.Resolve<ICloud>();

        }
    }
}
