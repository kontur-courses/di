using System.Drawing;
using TagsCloud.Interfaces;
using System.Collections.Generic;

namespace TagsCloud
{
    public class TagCloudVisualizer
    {
        private readonly IWordStream wordStream;
        private readonly ITagGenerator tagGenerator;
        private readonly ITagCloudGenerator tagCloudGenerator;
        private readonly ICloudDrawer cloudDrawer;
        private readonly IImageSaver imageSaver;
        private readonly IWordCounter wordCounter;
        private readonly TagCloudSettings tagCloudSettings;

        public TagCloudVisualizer(IWordStream wordStream,
            ITagGenerator tagGenerator,
            ITagCloudGenerator tagCloudGenerator,
            ICloudDrawer cloudDrawer,
            IImageSaver imageSaver,
            IWordCounter wordCounter,
            TagCloudSettings tagCloudSettings)
        {
            this.wordStream = wordStream;
            this.tagGenerator = tagGenerator;
            this.tagCloudGenerator = tagCloudGenerator;
            this.cloudDrawer = cloudDrawer;
            this.imageSaver = imageSaver;
            this.wordCounter = wordCounter;
            this.tagCloudSettings = tagCloudSettings;
        }

        public void GenerateTagCloud()
        {
            var words = wordStream.GetWords(tagCloudSettings.PathToInput);
            var wordStatistics = wordCounter.GetWordsStatistics(words);
            var tags = tagGenerator.GenerateTag(wordStatistics);
            var tagCloud = tagCloudGenerator.GenerateTagCloud(tags);
            using (var image = cloudDrawer.Paint(tagCloud, new Size(tagCloudSettings.WidthOutputImage, tagCloudSettings.HeightOutputImage), tagCloudSettings.BackgroundColor, 15))
                imageSaver.SaveImage(image, tagCloudSettings.PathToOutput, tagCloudSettings.imageFormat);
        }
    }
}
