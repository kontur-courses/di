using System.Drawing;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    class TagCloudVisualizer
    {
        private readonly IWordStream wordStream;
        private readonly ITagGenerator tagGenerator;
        private readonly ITagCloudGenerator tagCloudGenerator;
        private readonly ICloudDrawer cloudDrawer;
        private readonly IImageSaver imageSaver;
        private readonly IWordCounter wordCounter;

        public TagCloudVisualizer(IWordStream wordStream,
            ITagGenerator tagGenerator,
            ITagCloudGenerator tagCloudGenerator,
            ICloudDrawer cloudDrawer,
            IImageSaver imageSaver,
            IWordCounter wordCounter)
        {
            this.wordStream = wordStream;
            this.tagGenerator = tagGenerator;
            this.tagCloudGenerator = tagCloudGenerator;
            this.cloudDrawer = cloudDrawer;
            this.imageSaver = imageSaver;
            this.wordCounter = wordCounter;
        }

        public void GenerateTagCloud(string inputPath, string outputPath, Size sizeResultImage, Color backgroundColor)
        {
            var words = wordStream.GetWords(inputPath);
            var wordStatistics = wordCounter.getAllStatistics(words);
            var tags = tagGenerator.GenerateTag(wordStatistics);
            var tagCloud = tagCloudGenerator.GenerateTagCloud(tags);
            var image = cloudDrawer.Paint(tagCloud, sizeResultImage, backgroundColor, 15);
            imageSaver.SaveImage(image, outputPath);
        }
    }
}
