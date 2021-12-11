using LightInject;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Infrastructure
{
    public static class ContainerProvider
    {
        public static ServiceContainer GetContainer()
        {
            var container = new ServiceContainer();
            container.Register<IParser, TxtParser>();
            container.Register<IPreprocessorsApplier, PreprocessorsApplier>();
            container.Register<IFiltersApplier, FiltersApplier>();
            container.Register<ITagCreator, TagCreator>();
            container.Register<ITagPainter, GradientTagPainter>();

            container.Register<ISpiral, OvalSpiral>();
            container.Register<ICloudLayouter, OvalCloudLayouter>();

            container.Register<TagCloudPainter>();
            container.RegisterInstance(SettingsProvider.GetSettings());
            container.RegisterInstance(CloudSettingsProvider.GetSettings());
            container.Register<ImageSettingsAction>();
            container.Register<CloudSettingsAction>();
            container.Register<GenerateImageAction>();
            container.Register<ConsoleUI>();
            return container;
        }
    }
}
