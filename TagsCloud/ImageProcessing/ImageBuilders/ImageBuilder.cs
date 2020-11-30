using System.Collections.Generic;
using System.Drawing;
using TagsCloud.ImageProcessing.ImageBuilders;
using TagsCloud.TagsCloudProcessing;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.ImageProcessing.Config.ImageBuilders
{
    public class ImageBuilder : IImageBuilder
    {
        private readonly IImageConfig imageConfig;
        private readonly IWordsConfig wordsConfig;

        public ImageBuilder(IImageConfig imageConfig, IWordsConfig wordsConfig)
        {
            this.imageConfig = imageConfig;
            this.wordsConfig = wordsConfig;
        }

        public Bitmap BuildImage(IEnumerable<Tag> tags)
        {
            var image = new Bitmap(imageConfig.ImageSize.Width, imageConfig.ImageSize.Height);

            using var graphics = Graphics.FromImage(image);
            using var brush = new SolidBrush(wordsConfig.Color);

            foreach (var tag in tags)
                graphics.DrawString(tag.Value, tag.Font, brush, tag.Rectangle);

            return image;
        }
    }
}
