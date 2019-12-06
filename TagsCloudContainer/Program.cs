using System.Drawing;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using TagsCloudContainer.Core.ImageBuilder;
using TagsCloudContainer.Core.ImageSavers;
using TagsCloudContainer.Core.LayoutAlgorithms;
using TagsCloudContainer.Core.Readers;
using TagsCloudContainer.Core.TextHandler.WordFilters;
using TagsCloudContainer.Core.TextHandler.WordHandlers;
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
            container.Resolve<IUi>();
        }

        public static WindsorContainer BuildContainer(string[] args)
        {
            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, false));
            container.Register(Component.For<IUi>()
                .ImplementedBy<ConsoleUi>()
                .DependsOn(Dependency.OnValue("userInput", args)));
            container.Register(Component.For<IReader>()
                .ImplementedBy<FileReader>());
            container.Register(Component.For<ILayoutAlgorithm>()
                .ImplementedBy<CircularCloudLayouter>()
                .DependsOn(Dependency.OnValue("center", new Point(500, 500))));
            container.Register(Component.For<IImageBuilder>()
                .ImplementedBy<TagCloudImageCreator>()
                .DependsOn(Dependency.OnValue("wordBrush", new SolidBrush(Color.Blue))));
            container.Register(Component.For<IWordFilter>().ImplementedBy<BoringWordsFilter>());
            container.Register(
                Component.For<IWordHandler>().ImplementedBy<LowerCaseHandler>(),
                                 Component.For<IWordHandler>().ImplementedBy<PunctuationRemover>());
            container.Register(Component.For<IImageSaver>()
                .ImplementedBy<PngSaver>());
            container.Register(Component.For<Filter>()
                .LifestyleSingleton());
            container.Register(Component.For<Handler>()
                .LifestyleSingleton());
            return container;
        }
    }
}