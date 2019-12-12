using System.Drawing;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using TagsCloudContainer.Core.ColoringAlgorithms;
using TagsCloudContainer.Core.ImageBuilder;
using TagsCloudContainer.Core.ImageSavers;
using TagsCloudContainer.Core.LayoutAlgorithms;
using TagsCloudContainer.Core.Readers;
using TagsCloudContainer.Core.TextHandler.WordConverters;
using TagsCloudContainer.Core.TextHandler.WordFilters;
using TagsCloudContainer.Core.UserInterfaces;
using TagsCloudContainer.Core.UserInterfaces.ConsoleUI;
using Component = Castle.MicroKernel.Registration.Component;

namespace TagsCloudContainer
{
    class Program
    {
        public static void Main(string[] args)
        {
            var container = BuildContainer(args);
            var ui = container.Resolve<IUi>();
            ui.Run(args);
        }

        public static WindsorContainer BuildContainer(string[] args)
        {
            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, false));
            container.Register(Component.For<IUi>()
                .ImplementedBy<ConsoleUi>());
            container.Register(
                Component.For<IReader>().ImplementedBy<TxtReader>(),
                Component.For<IReader>().ImplementedBy<DocReader>());
            container.Register(Component.For<ILayoutAlgorithm>()
                .ImplementedBy<CircularCloudLayouter>()
                .DependsOn(Dependency.OnValue("center", new Point(500, 500))));
            container.Register(Component.For<IImageBuilder>()
                .ImplementedBy<TagCloudImageBuilder>());
            container.Register(Component.For<IColoringAlgorithm>()
                .ImplementedBy<RandomColoring>());
            container.Register(Component.For<IWordFilter>().ImplementedBy<BoringWordsFilter>());
            container.Register(
                Component.For<IWordConverter>().ImplementedBy<LowerCaseConverter>(),
                Component.For<IWordConverter>().ImplementedBy<PunctuationRemover>());
            container.Register(Component.For<IImageSaver>()
                .ImplementedBy<ImageSaver>());
            container.Register(Component.For<Filter>()
                .LifestyleSingleton());
            container.Register(Component.For<WordConverter>()
                .LifestyleSingleton());
            return container;
        }
    }
}