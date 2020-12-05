using Cloud.ClientUI.ArgumentConverters;
using CloudContainer;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.ClientUI
{
    public class TagCloudCreator
    {
        private readonly IArgumentConverter argumentConverter;
        private readonly Arguments arguments;
        private readonly TagCloudContainerBuilder container;
        private readonly ISaver saver;

        public TagCloudCreator(TagCloudContainerBuilder container, IArgumentConverter argumentConverter,
            Arguments arguments,
            ISaver saver)
        {
            this.container = container;
            this.argumentConverter = argumentConverter;
            this.arguments = arguments;
            this.saver = saver;
        }

        public void Run()
        {
            var convertedArguments = argumentConverter.ParseArguments(arguments);
            var visualizationContainer = container.CreateTagCloudContainer(convertedArguments);
            var image = visualizationContainer.BuildServiceProvider().GetService<TagCloudContainer>()
                .GetImage(convertedArguments);
            saver.SaveImage(image, convertedArguments.OutputFileName);
        }
    }
}