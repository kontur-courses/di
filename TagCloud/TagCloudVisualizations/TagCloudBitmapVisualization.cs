using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using TagCloud.CloudLayouters;
using TagCloud.TagCloudCreators;
using TagCloud.Tags;
using TagCloud.WordPreprocessors;

namespace TagCloud.TagCloudVisualizations
{
    public abstract class TagCloudBitmapVisualization : ITagCloudVisualization
    {
        private readonly Random random = new Random();
        private readonly ICloudLayouter.Factory cloudLayouterFactory;
        private readonly IWordPreprocessor wordPreprocessor;
        private readonly ITagCloudCreator.Factory tagCloudCreatorFactory;
        private TagCloud tagCloud;

        public TagCloudBitmapVisualization(ICloudLayouter.Factory cloudLayouterFactory,
            IWordPreprocessor wordPreprocessor, ITagCloudCreator.Factory tagCloudCreatorFactory)
        {
            this.cloudLayouterFactory = cloudLayouterFactory;
            this.wordPreprocessor = wordPreprocessor;
            this.tagCloudCreatorFactory = tagCloudCreatorFactory;
        }

        public void PrepareImage(Graphics inGraphics,
            ITagCloudVisualizationSettings settings)
        {
            tagCloud = tagCloudCreatorFactory(cloudLayouterFactory, wordPreprocessor, settings).TagCloud;

            var bitmap = CreateTagCloudBitmap(settings);
            
            var rect = new Rectangle(0, 0, settings.PictureSize.Value.Width, settings.PictureSize.Value.Height);
            inGraphics.CompositingMode = CompositingMode.SourceCopy;
            inGraphics.CompositingQuality = CompositingQuality.HighQuality;
            inGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            inGraphics.SmoothingMode = SmoothingMode.HighQuality;
            inGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            inGraphics.DrawImage(bitmap, rect);
        }
        
        public void Save(string file,
            ITagCloudVisualizationSettings settings)
        {
            tagCloud = tagCloudCreatorFactory(cloudLayouterFactory, wordPreprocessor, settings).TagCloud;

            var bitmap = CreateTagCloudBitmap(settings);

            if (settings.PictureSize != null)
                bitmap = new Bitmap(bitmap, settings.PictureSize.Value);

            bitmap.Save(file, ImageFormat.Png);
        }

        private Bitmap CreateTagCloudBitmap(ITagCloudVisualizationSettings settings)
        {
            var rectangleOutline = 1;
            var bitmap = new Bitmap(
                tagCloud.GetWidth() + rectangleOutline,
                tagCloud.GetHeight() + rectangleOutline);

            var frameShift = new Size(-tagCloud.GetLeftBound(), -tagCloud.GetTopBound());
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(settings.BackgroundColor);
            foreach (var tag in tagCloud.Layouts)
            {
                tag.ShiftTo(frameShift);
                //TODO: переделать
                DrawIn(graphics, tag, settings.TextColor == null
                    ? GetRandomBrush(settings.BackgroundColor)
                    : new SolidBrush(settings.TextColor.Value));
            }

            return bitmap;
        }

        public virtual void DrawIn(Graphics graphics, ITag tag, Brush byBrush)
        {
            throw new NotImplementedException();
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