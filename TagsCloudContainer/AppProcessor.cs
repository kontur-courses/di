using System.Linq;
using RectanglesCloudLayouter.Interfaces;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class AppProcessor : IAppProcessor
    {
        private readonly IVisualization _visualization;
        private readonly IWordTagsLayouter _wordTagsLayouter;
        private readonly ICloudRadiusCalculator _cloudRadiusCalculator;
        private readonly IFileReader _fileReader;
        private readonly IImageSaver _imageSaver;

        public AppProcessor(IVisualization visualization,
            IWordTagsLayouter wordTagsLayouter, ICloudRadiusCalculator cloudRadiusCalculator, IFileReader fileReader,
            IImageSaver imageSaver)
        {
            _visualization = visualization;
            _wordTagsLayouter = wordTagsLayouter;
            _cloudRadiusCalculator = cloudRadiusCalculator;
            _fileReader = fileReader;
            _imageSaver = imageSaver;
        }

        public void Run()
        {
            var originalText = _fileReader.GetTextFromFile();
            var wordTagsLayouter = _wordTagsLayouter.GetWordTags(originalText).ToList();
            using var cloudImage =
                _visualization.GetImageCloud(_cloudRadiusCalculator.CloudRadius, wordTagsLayouter.ToList());
            _imageSaver.Save(cloudImage);
        }
    }
}