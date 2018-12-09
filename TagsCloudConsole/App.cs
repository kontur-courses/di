using TagsCloudVisualization;
using TagsCloudVisualization.ImageSaving;
using TagsCloudVisualization.Visualizing;

namespace TagsCloudConsole
{
    class App
    {
        private readonly IFileReader reader;
        private readonly CloudBuilder cloudBuilder;
        private readonly TagsCloudVisualizer cloudVisualizer;
        private readonly ImageSaverSelector imageSaverSelector;
        private readonly string outputFileName;
        private readonly string extension;
        private readonly string wordsFileName;

        public App(IFileReader reader, CloudBuilder cloudBuilder, TagsCloudVisualizer cloudVisualizer,
            ImageSaverSelector imageSaverSelector, string outputFileName, string extension, string wordsFileName)
        {
            this.reader = reader;
            this.cloudBuilder = cloudBuilder;
            this.cloudVisualizer = cloudVisualizer;
            this.outputFileName = outputFileName;
            this.wordsFileName = wordsFileName;
            this.imageSaverSelector = imageSaverSelector;
            this.extension = extension;
        }

        public void Run()
        {
            var words = reader.ReadAllWords(wordsFileName);
            var tagCloud = cloudBuilder.BuildTagCloud(words);
            var picture = cloudVisualizer.GetPictureOfRectangles(tagCloud);
            imageSaverSelector
                .SelectImageSaver(extension)
                .SaveImage(picture, extension, outputFileName);
        }
    }
}
