using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.Generation;
using TagsCloudContainer.Infrastructure.PointTracks;

namespace TagsCloudContainer.Visualization
{
    public class TagsCloudVisualizer : IVisualizer
    {
        public Bitmap Visualize(
            IEnumerable<string> tags,
            TagsCloudSettings settings)
        {
            var imageSize = settings.ImageSize;
            var image = new Bitmap(imageSize.Width, imageSize.Height);
            
            using (var graphics = Graphics.FromImage(image))
            {
                var imageCenter = new Point(image.Size.Width / 2, image.Size.Height / 2);

                graphics.Clear(settings.BackgroundColor);
                DrawTags(tags, graphics, imageCenter, settings);

                return image;
            }
        }

        private void DrawTags(
            IEnumerable<string> tags,
            Graphics graphics,
            Point imageCenter,
            TagsCloudSettings settings)
        {
            var font = settings.Font;
            var layouter = new RectangleLayouter(imageCenter, settings.LayoutTrack);

            foreach (var tag in tags)
            {
                var container = GetContainerFor(tag, font,
                    settings.TagContainerPadding, layouter);

                DrawString(tag, font, graphics, settings.TextColor, container);

                if (font.Size >= 9)
                    font = new Font(font.FontFamily, font.Size - 2);
            }
        }

        private void DrawString(string str, Font font, Graphics graphics, Color color, Rectangle container)
        {
            var brush = new SolidBrush(color);
            graphics.DrawString(str, font, brush, container.GetCenter(),
                new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });
        }

        private Rectangle GetContainerFor(string tag, Font font, int padding,
            RectangleLayouter layouter)
        {
            var size = TextRenderer.MeasureText(tag, font) + new Size(padding, padding);
            return layouter.PutNextRectangle(size);
        }
    }
}