using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TagsCloud
{
    public class TagCloudGenerator : ITagCloudGenerator
    {
        public Bitmap GenerateTagCloud(IEnumerable<string> words, IImageSettings imageSettings)
        {
            var tagCloudImage = new Bitmap(imageSettings.Width, imageSettings.Height);
            using (var graphics = Graphics.FromImage(tagCloudImage))
            {
                graphics.Clear(imageSettings.BackgroundColor);
                var font = imageSettings.GetFont();
                var brush = new SolidBrush(imageSettings.FontColor);

                int yOffset = 0;
                foreach (var word in words)
                {
                    graphics.DrawString(word, font, brush, new PointF(0, yOffset));
                    yOffset += font.Height;
                }
            }

            return tagCloudImage;
        }
    }
}

