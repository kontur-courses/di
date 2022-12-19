using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using TagCloud.Tags;

namespace TagCloud.TagCloudVisualizations
{
    public abstract class TagCloudBitmapVisualization : ITagCloudVisualization
    {
        private readonly Random random = new Random();
        
        public void PrepareImage(Graphics inGraphics,
            ITagCloud tagCloud,
            ITagCloudVisualizationSettings settings)
        {
            var bitmap = CreateOriginSizeTagCloudBitmap(tagCloud, settings);

            if (!settings.PictureSize.HasValue)
                inGraphics.DrawImage(bitmap, new Point());
            else
            {
                var rect = new Rectangle(0, 0, settings.PictureSize.Value.Width, settings.PictureSize.Value.Height);
                inGraphics.CompositingMode = CompositingMode.SourceCopy;
                inGraphics.CompositingQuality = CompositingQuality.HighQuality;
                inGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                inGraphics.SmoothingMode = SmoothingMode.HighQuality;
                inGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                inGraphics.DrawImage(bitmap, rect);
            }
        }
        
        public void SaveWithOriginSize(string file,
            ITagCloud tagCloud,
            ITagCloudVisualizationSettings settings)
        {
            var bitmap = CreateOriginSizeTagCloudBitmap(tagCloud, settings);

            bitmap.Save(file, ImageFormat.Png);
        }

        private Bitmap CreateOriginSizeTagCloudBitmap(ITagCloud tagCloud, ITagCloudVisualizationSettings settings)
        {
            var rectangleOutline = 1;
            var bitmap = new Bitmap(
                tagCloud.GetWidth() + rectangleOutline,
                tagCloud.GetHeight() + rectangleOutline);

            var frameShift = new Size(-tagCloud.GetLeftBound(), -tagCloud.GetTopBound());
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(settings.BackgroundColor);
            SolidBrush brush = settings.TextColor != null
                ? new SolidBrush(settings.TextColor.Value)
                : null;
            foreach (var tag in tagCloud.Layouts)
            {
                tag.ShiftTo(frameShift);
                DrawIn(graphics, tag, brush ?? GetRandomBrush(settings.BackgroundColor));
            }

            return bitmap;
        }

        protected virtual void DrawIn(Graphics graphics, ITag tag, Brush byBrush)
        {
            graphics.DrawRectangle(new Pen(byBrush), tag.Frame);
        }
        
        private Brush GetRandomBrush(Color? excludingColor) =>
            new SolidBrush(GetRandomColor(excludingColor));
        
        private Color GetRandomColor(Color? excludingColor)
        {
            var knownColors = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            var randomColorName = knownColors[random.Next(knownColors.Length)];
            var randomColor = Color.FromKnownColor(randomColorName);
            return excludingColor == null || randomColor != excludingColor 
                ? randomColor
                : GetRandomColor(excludingColor);
        }
    }
}