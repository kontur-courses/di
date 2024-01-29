using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Client;
using TagsCloudContainer.Infrastucture.Extensions;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.Visualization;

namespace TagsCloudContainer.Actions
{
    public class GUITagCloudClient : ITagCloudClient
    {
        private readonly PictureBox pictureBox;
        private readonly ImageSettings imageSettings;
        private readonly IDrawer drawer;
        private readonly ICloudLayouter cloudLayouter;
        private IWordProcessor wordProcessor;

        public GUITagCloudClient(PictureBox pictureBox, ImageSettings imageSettings,
            IDrawer drawer, ICloudLayouter cloudLayouter, IWordProcessor wordProcessor)
        {
            this.pictureBox = pictureBox;
            this.imageSettings = imageSettings;
            this.drawer = drawer;
            this.cloudLayouter = cloudLayouter;
            this.wordProcessor = wordProcessor;
        }

        public void DrawImage(string sourceFilePath, string boringFilePath)
        {
            var wordsCount = wordProcessor.CalculateFrequencyInterestingWords(sourceFilePath, boringFilePath);
            var rectangles = cloudLayouter.GetRectangles(wordsCount);
            drawer.Draw(rectangles);
        }

        public void SaveImage(string filePath)
        {
            pictureBox.SaveImage(filePath);
        }
    }
}