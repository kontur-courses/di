using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class TagCloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;

        public TagCloudPainter(IImageHolder imageHolder, ImageSettings imageSettings)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
        }

        public void Paint(IEnumerable<Word> words)
        {
            var imageSize = imageSettings.ImageSize;
            imageHolder.RecreateImage(imageSize);
            using var graphics = imageHolder.StartDrawing();
            foreach (var word in words.Where(x => x.Rectangle.IsNestedInImage(imageSize)))
                graphics.DrawString(word.Text, word.Font, new SolidBrush(imageSettings.GetColor()),
                    word.Rectangle.Location);
        }
    }
}