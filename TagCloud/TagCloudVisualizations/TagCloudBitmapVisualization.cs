using System;
using System.Drawing;
using TagCloud.TagCloudCreators;

namespace TagCloud.TagCloudVisualizations
{
    public class TagCloudBitmapVisualization : ITagCloudVisualization
    {
        private readonly Random random = new Random();
        private readonly ITagCloudCreator tagCloudCreator;
        private TagCloud tagCloud;

        public TagCloudBitmapVisualization(ITagCloudCreator tagCloudCreator)
        {
            this.tagCloudCreator = tagCloudCreator;
        }

        public void PrepareImage(Graphics inGraphics, ITagCloudVisualizationSettings settings)
        {
            PrepareTagCloud(settings);
            
            var frameShift = new Size();//-tagCloud.GetLeftBound(), -tagCloud.GetTopBound());
            inGraphics.Clear(settings.BackgroundColor);

            using var graphics = inGraphics;
            foreach (var tag in tagCloud.Layouts)
            {
                tag.ShiftTo(frameShift);
                var byBrush = GetRandomBrush();
                tag.DrawIn(graphics, byBrush);
            }
        }

        public void Save(string file, ITagCloudVisualizationSettings settings)
        {
            var rectangleOutline = 1;

            PrepareTagCloud(settings);

            var bitmap = new Bitmap(
                tagCloud.GetWidth() + rectangleOutline,
                tagCloud.GetHeight() + rectangleOutline);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                PrepareImage(graphics, settings);
            }

            if (settings.PictureSize != null)
                bitmap = new Bitmap(bitmap, settings.PictureSize.Value);

            bitmap.Save(file, settings.PictureFormat);
        }

        private void PrepareTagCloud(ITagCloudVisualizationSettings settings)
        {
            tagCloud = tagCloudCreator.GenerateTagCloud(settings);
        }

        private Brush GetRandomBrush() =>
            new SolidBrush(GetRandomColor());
        
        private Color GetRandomColor()
        {
            var knownColors = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            var randomColorName = knownColors[random.Next(knownColors.Length)];
            return Color.FromKnownColor(randomColorName);
        }
    }
}