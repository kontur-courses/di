using System.Collections.Generic;
using System.Drawing;
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

        public Result<None> Paint(IEnumerable<Word> words)
        {
            var imageSize = imageSettings.ImageSize;
            imageHolder.RecreateImage(imageSize);
            using var graphics = imageHolder.StartDrawing();
            foreach (var word in words)
            {
                if (!word.Rectangle.IsNestedInImage(imageSize))
                    return Result.Fail<None>("Image is too small for tag cloud. Please set more image size");
                graphics.DrawString(word.Text, word.Font, new SolidBrush(imageSettings.GetColor()),
                    word.Rectangle.Location);
            }

            return Result.Ok();
        }
    }
}