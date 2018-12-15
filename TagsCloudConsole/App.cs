using System;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudGenerating;
using TagsCloudVisualization.ImageSaving;
using TagsCloudVisualization.Preprocessing;
using TagsCloudVisualization.Utils;
using TagsCloudVisualization.Visualizing;
using TagsCloudVisualization.WordsFileReading;

namespace TagsCloudConsole
{
    class App
    {
        private readonly Preprocessor preprocessor;
        private readonly ITagsCloudGenerator tagsCloudGenerator;
        private readonly TagsCloudVisualizer cloudVisualizer;
        private readonly FileReaderSelector fileReaderSelector;
        private readonly ParserSelector parserSelector;
        private readonly ImageSaverSelector imageSaverSelector;

        public App(
            FileReaderSelector fileReaderSelector, 
            ParserSelector parserSelector,
            Preprocessor preprocessor,
            ITagsCloudGenerator tagsCloudGenerator,
            TagsCloudVisualizer cloudVisualizer,
            ImageSaverSelector imageSaverSelector)
        {
            this.fileReaderSelector = fileReaderSelector;
            this.parserSelector = parserSelector;
            this.preprocessor = preprocessor;
            this.tagsCloudGenerator = tagsCloudGenerator;
            this.cloudVisualizer = cloudVisualizer;
            this.imageSaverSelector = imageSaverSelector;
        }

        public void Run(string imageFileName, string wordsFileName, string mode)
        {
            var reader = fileReaderSelector.SelectFileReader(wordsFileName);
            var parser = parserSelector.SelectParser(mode);
            var imageSaver = imageSaverSelector.SelectImageSaver(imageFileName);

            var words = parser.ParseText(
                reader.ReadText(wordsFileName));
            var wordsStatistics = new StatisticsCalculator()
                .CalculateStatistics(preprocessor.Preprocess(words));
            
            var tagCloud = tagsCloudGenerator.GenerateTagsCloud(wordsStatistics);

            var picture = cloudVisualizer.GetPictureOfRectangles(tagCloud);
            imageSaver.SaveImage(picture, imageFileName);
        }
    }
}
