using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TagsCloudContainer.Rendering
{
    public interface ITagsCloudRenderer
    {
        void Render(IEnumerable<WordStyle> words, Size imageSize);
    }

    public class TabsCloudRenderer : ITagsCloudRenderer
    {
        private readonly RenderConfig renderConfig;

        public TabsCloudRenderer(RenderConfig renderConfig)
        {
            this.renderConfig = renderConfig ?? throw new ArgumentNullException(nameof(renderConfig));
        }

        public void Render(IEnumerable<WordStyle> words, Size imageSize)
        {
            using var canvas = CreateCanvas(imageSize);

            canvas.Graphics.FillRectangle(renderConfig.Background, 0, 0, imageSize.Width, imageSize.Height);

            foreach (var word in words)
                canvas.Graphics.DrawString(
                    word.Value, word.Font, word.Brush, word.Location, StringFormat.GenericTypographic);

            canvas.Graphics.Save();
            canvas.Bitmap.Save(renderConfig.OutputFile, renderConfig.ImageFormat);
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
            if (renderConfig.DesiredImageSize == null)
                return (renderConfig.Scale ?? 1, renderConfig.Scale ?? 1);

            var scaleX = (float)renderConfig.DesiredImageSize.Value.Width / imageSize.Width;
            var scaleY = (float)renderConfig.DesiredImageSize.Value.Height / imageSize.Height;

            return (scaleX, scaleY);
        }
    }
}