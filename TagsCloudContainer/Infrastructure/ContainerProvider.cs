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
            container.Register<ITagPainter, TagPainter>();

            container.RegisterInstance(new System.Drawing.Point(500, 500));
            container.Register<ISpiral, ArchimedeanSpiral>();
            container.Register<ICloudLayouter, OvalCloudLayouter>();

            container.Register<TagCloudPainter>();
            container.RegisterInstance(SettingsProvider.GetSettings());
            return container;
        }
    }
}
