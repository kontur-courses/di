using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Drawable;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.ImageCreators
{
    public class ImageCreator : IImageCreator
    {
        public Image Draw(IEnumerable<IDrawable> layout)
        {
            var size = layout.GetMinCanvasSize();
            var bitmap = new Bitmap(size.Width, size.Height);
            using var graphics = Graphics.FromImage(bitmap);

            graphics.TranslateTransform(bitmap.Width / 2f, bitmap.Height / 2f);

            foreach (var drawableTag in layout)
                drawableTag.Draw(graphics);

            return bitmap;
        }
    }
}