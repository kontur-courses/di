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
            container.Register<ITagComposer, TagComposer>();
            container.Register<ITagPainter, GradientTagPainter>();

            container.Register<ISpiral, OvalSpiral>();
            container.Register<ICloudLayouter, OvalCloudLayouter>();

            container.Register<TagCloudPainter>();
            container.RegisterInstance(SettingsProvider.GetSettings());
            container.Register<ConsoleUI>();
            //container.Register<Settings>(factory);
            return container;
        }
    }
}
