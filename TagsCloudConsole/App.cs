using TagsCloudVisualization;
using TagsCloudVisualization.ImageSaving;
using TagsCloudVisualization.Visualizing;
using TagsCloudVisualization.WordsFileReading;

namespace TagsCloudConsole
{
    class App
    { 
        private readonly CloudBuilder cloudBuilder;
        private readonly TagsCloudVisualizer cloudVisualizer;
        private readonly string imageFileName;
        private readonly string imageFileExtension;
        private readonly string wordsFileName;
        private readonly string wordsFileExtension;
        private readonly FileReaderSelector fileReaderSelector;
        private readonly ImageSaverSelector imageSaverSelector;

        public App(FileReaderSelector fileReaderSelector, CloudBuilder cloudBuilder, TagsCloudVisualizer cloudVisualizer,
            ImageSaverSelector imageSaverSelector, string imageFileName, 
            string imageFileExtension, string wordsFileName, string wordsFileExtension)
        {
            this.fileReaderSelector = fileReaderSelector;
            this.cloudBuilder = cloudBuilder;
            this.cloudVisualizer = cloudVisualizer;
            this.imageFileName = imageFileName;
            this.wordsFileName = wordsFileName;
            this.wordsFileExtension = wordsFileExtension;
            this.imageFileExtension = imageFileExtension;
            this.imageSaverSelector = imageSaverSelector;
        }

        public void Run()
        {
            var reader = fileReaderSelector.SelectFileReader(wordsFileExtension);
            var imageSaver = imageSaverSelector.SelectImageSaver(imageFileExtension);
            var words = reader.ReadAllWords(wordsFileName, wordsFileExtension);
            var tagCloud = cloudBuilder.BuildTagCloud(words);
            var picture = cloudVisualizer.GetPictureOfRectangles(tagCloud);
            imageSaver.SaveImage(picture, imageFileExtension, imageFileName);
        }
    }
}
