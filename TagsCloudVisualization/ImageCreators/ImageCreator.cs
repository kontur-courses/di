using System.Drawing;
using TagsCloudVisualization.DrawableContainers;

namespace TagsCloudVisualization.ImageCreators
{
    public class ImageCreator : IImageCreator
    {
        public Image Draw(IDrawableContainer drawableContainer)
        {
            var size = drawableContainer.GetMinCanvasSize();
            var bitmap = new Bitmap(size.Width, size.Height);
            using var graphics = Graphics.FromImage(bitmap);

            graphics.TranslateTransform(bitmap.Width / 2f, bitmap.Height / 2f);

            foreach (var drawableTag in drawableContainer.GetDrawables())
                drawableTag.Draw(graphics);

            return bitmap;
        }
    }
}