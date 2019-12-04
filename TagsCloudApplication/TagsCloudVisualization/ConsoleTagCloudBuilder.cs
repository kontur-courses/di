namespace TagsCloudVisualization
{
    public class ConsoleTagCloudBuilder
    {
        private readonly CloudTagProvider tagProvider;
        private readonly TagCloudVisualizator visualizator;
        private readonly ImageSaver imageSaver;
        private readonly Options options;

        public ConsoleTagCloudBuilder(
            CloudTagProvider tagProvider, 
            TagCloudVisualizator visualizator,
            ImageSaver imageSaver,
            Options opts)
        {
            this.tagProvider = tagProvider;
            this.visualizator = visualizator;
            this.imageSaver = imageSaver;
            options = opts;
        }

        public void Run()
        {
            var cloudTags = tagProvider.ReadCloudTags(options.InputFilename);
            var image = visualizator.VisualizeCloudTags(cloudTags);
            imageSaver.SaveImage(image, options.OutputFilename);
        }
    }
}
