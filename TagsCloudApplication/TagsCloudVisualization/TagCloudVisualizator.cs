using System.Drawing;
using TagsCloudLayout.CloudLayouters;
using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public class TagCloudVisualizator : ITagCloudVisualizator
    {
        private readonly VisualizatorProperties properties;
        private readonly ITextColorProvider colorGenerator;
        private readonly ICloudLayouter layouter;

        public TagCloudVisualizator(
            VisualizatorProperties properties,
            ITextColorProvider colorGenerator,
            ICloudLayouter layouter)
        {
            this.properties = properties;
            this.colorGenerator = colorGenerator;
            this.layouter = layouter;
        }

        public Bitmap VisualizeCloudTags(IReadOnlyCollection<CloudTag> cloudTags)
        {
            var bitmap = new Bitmap(properties.ImageSize.Width, properties.ImageSize.Height);
            var graphics = Graphics.FromImage(bitmap);

            foreach(var cloudTag in cloudTags)
            {
                var boundingBoxSize = graphics.MeasureString(cloudTag.Word, cloudTag.Font).ToSize();

                var rectangle = layouter.PutNextRectangle(boundingBoxSize);
                graphics.DrawString(cloudTag.Word,
                    cloudTag.Font, 
                    new SolidBrush(colorGenerator.GetTextColor(cloudTag.Word, rectangle)), 
                    rectangle.Location);
            }

            return bitmap;
        }
    }
}
