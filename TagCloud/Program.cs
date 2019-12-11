using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using TagCloud.Actions;
using TagCloud.IServices;

namespace TagCloud
{
    public class Program
    {
        public static void Main()
        {
            var container = GetContainer();
            var visualization = container.Resolve<ICloudVisualization>();
            var client = container.Resolve<IClient>();
            client.Start(visualization);
        }

        public static WindsorContainer GetContainer()
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));
            container.Register(Component.For<IPaletteDictionaryFactory>().ImplementedBy<PaletteDictionaryFactory>()
                .LifestyleSingleton());
            container.Register(Component.For<IFontSettingsFactory>().ImplementedBy<FontSettingsFactory>()
                .LifestyleSingleton());
            container.Register(Component.For<IWordsToTagsParser>().ImplementedBy<WordsToTagsParser>());
            container.Register(Component.For<IWordsHandler>().ImplementedBy<WordsHandler>());
            container.Register(Component.For<ITagCollectionFactory>().ImplementedBy<TagCollectionFactory>()
                .LifestyleSingleton());
            container.Register(Component.For<IAlgorithm>().ImplementedBy<ArchimedianSprial>());
            container.Register(Component.For<ICircularCloudLayouter>().ImplementedBy<CircularCloudLayouter>());
            container.Register(Component.For<ICloud>().ImplementedBy<Cloud>());
            container.Register(Component.For<ICloudVisualization>().ImplementedBy<CloudVisualization>());
            container.Register(Component.For<IPaletteNamesFactory>().ImplementedBy<PaletteNamesFactory>()
                .LifestyleSingleton());
            container.Register(Component.For<IAction>().ImplementedBy<NewImageAction>().LifestyleSingleton());
            container.Register(Component.For<IAction>().ImplementedBy<SaveImageAction>().LifestyleSingleton());
            container.Register(Component.For<IAction>().ImplementedBy<ShowImageAction>().LifestyleSingleton());
            container.Register(Component.For<IAction>().ImplementedBy<ExitAction>().LifestyleSingleton());
            container.Register(Component.For<IClient>().ImplementedBy<Client>());
            return container;
        }
    }
}