using Cloud.ClientUI.ArgumentConverters;
using CloudContainer;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.ClientUI
{
    public class ContainerBuilder
    {
        public ServiceCollection CreateContainer(Arguments arguments)
        {
            var container = new ServiceCollection();
            container.AddSingleton<TagCloudContainerBuilder, TagCloudContainerBuilder>();
            container.AddSingleton<ISaver, PngSaver>();
            container.AddSingleton<IArgumentConverter, ArgumentConverter>();
            container.AddSingleton(typeof(Arguments), arguments);
            container.AddSingleton<TagCloudCreator, TagCloudCreator>();
            container.AddSingleton<TagCloudArguments, TagCloudArguments>();
            return container;
        }
    }
}