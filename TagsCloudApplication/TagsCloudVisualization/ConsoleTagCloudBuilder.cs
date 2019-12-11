namespace TagsCloudVisualization
{
    public class ConsoleTagCloudBuilder
    {
        private readonly ITagProvider tagProvider;
        private readonly ITagCloudVisualizator visualizator;
        private readonly ITagCloudBuilderProperties properties;

        public ConsoleTagCloudBuilder(
            ITagProvider tagProvider, 
            ITagCloudVisualizator visualizator,
            ITagCloudBuilderProperties properties)
        {
            this.tagProvider = tagProvider;
            this.visualizator = visualizator;
            this.properties = properties;
        }

        public void Run()
        {
            var cloudTags = tagProvider.ReadCloudTags(properties.InputFilename);
            var image = visualizator.VisualizeCloudTags(cloudTags);
            ImageUtilities.SaveImage(image, properties.OutputFormat, properties.OutputFilename);
        }
    }
}
