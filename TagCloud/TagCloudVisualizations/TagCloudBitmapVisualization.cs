using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using TagCloud.TagCloudCreators;

namespace TagCloud.TagCloudVisualizations
{
    public class TagCloudBitmapVisualization : ITagCloudVisualization
    {
        private readonly Random random = new Random();
        private readonly ITagCloudCreator tagCloudCreator;

        public TagCloudBitmapVisualization(ITagCloudCreator tagCloudCreator)
        {
            this.tagCloudCreator = tagCloudCreator;
        }

        public void Visualize(ITagCloudVisualizationSettings settings)
        {
            throw new NotImplementedException();
        }

        public void Save(string file, ITagCloudVisualizationSettings settings)
        {
            var rectangleOutline = 1;

            var tagCloud = tagCloudCreator.GenerateTagCloud(settings);

            var bitmap = new Bitmap(
                tagCloud.GetWidth() + rectangleOutline, 
                tagCloud.GetHeight() + rectangleOutline);

            var frameShift = new Size(-tagCloud.GetLeftBound(), -tagCloud.GetTopBound());

            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (var tag in tagCloud.Layouts)
                {
                    tag.ShiftTo(frameShift);
                    var byBrush = GetRandomBrush();
                    tag.DrawIn(graphics, byBrush);
                }
            }

            if (settings.PictureSize != null)
                bitmap = new Bitmap(bitmap, settings.PictureSize.Value);
            bitmap.Save(file, settings.PictureFormat);
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