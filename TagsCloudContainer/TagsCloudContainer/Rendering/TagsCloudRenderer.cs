using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Rendering
{
    public interface ITagsCloudRenderer
    {
        Bitmap GetBitmap(IEnumerable<WordStyle> words, Size imageSize);
    }

    public class TagsCloudRenderer : ITagsCloudRenderer
    {
        private readonly IRenderingSettings renderingSettings;

        public TagsCloudRenderer(IRenderingSettings renderingSettings)
        {
            this.renderingSettings = renderingSettings ?? throw new ArgumentNullException(nameof(renderingSettings));
        }

        public Bitmap GetBitmap(IEnumerable<WordStyle> words, Size imageSize)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            using var canvas = CreateCanvas(imageSize);
            canvas.Graphics.FillRectangle(renderingSettings.Background, 0, 0, imageSize.Width, imageSize.Height);
            foreach (var word in words)
                canvas.Graphics.DrawString(
                    word.Value, word.Font, word.Brush, word.Location, StringFormat.GenericTypographic);

            canvas.Graphics.Save();
            return new Bitmap(canvas.Bitmap);
        }

        private Canvas CreateCanvas(Size imageSize)
        {
            var (scaleX, scaleY) = GetScale(imageSize);
            var bitmap = new Bitmap((int)(imageSize.Width * scaleX), (int)(imageSize.Height * scaleY));
            var graphics = Graphics.FromImage(bitmap);
            graphics.ScaleTransform(scaleX, scaleY);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            return new Canvas(bitmap, graphics);
        }

        private (float scaleX, float scaleY) GetScale(Size imageSize)
        {
            if (renderingSettings.DesiredImageSize == null)
                return (renderingSettings.Scale, renderingSettings.Scale);

            var scaleX = (float)renderingSettings.DesiredImageSize.Value.Width / imageSize.Width;
            var scaleY = (float)renderingSettings.DesiredImageSize.Value.Height / imageSize.Height;

            return (scaleX, scaleY);
        }
    }
}