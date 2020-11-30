using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.App
{
    public class TagCloudPainter
    {
        private readonly ImageHolder imageHolder;
        private readonly ImageSettings imageSettings;

        public TagCloudPainter(ImageHolder imageHolder, ImageSettings imageSettings)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
        }

        public void Paint(List<Word> words)
        {
            imageHolder.RecreateImage(imageSettings.ImageSize);
            using var graphics = imageHolder.StartDrawing();
            foreach (var word in words)
                //todo check that word can be drawn in image
                graphics.DrawString(word.Text, word.Font, new SolidBrush(imageSettings.GetColor()), word.Rectangle);
        }
    }
}