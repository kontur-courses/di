using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using TagsCloud.CloudConstruction;
using TagsCloud.Visualization.ColorDefiner;

namespace TagsCloud.Visualization
{
    public class Visualizer : IVisualizer
    {
        public readonly string FontName;
        public readonly Color BackgroundColor;
        public readonly IColorDefiner ColorDefiner;

        public Visualizer(string fontName, Color backgroundColor, IColorDefiner colorDefiner)
        {
            FontName = fontName;
            BackgroundColor = backgroundColor;
            ColorDefiner = colorDefiner;
        }

        public  Bitmap GetCloudVisualization( IEnumerable<Tag.Tag> tags)
        {
            var imgRectangle = GetImageRectangle(tags);
            var bitmap = new Bitmap(imgRectangle.Width, imgRectangle.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(BackgroundColor);
                graphics.TranslateTransform(-imgRectangle.X, -imgRectangle.Y);
                foreach (var tag in tags)
                {
                    Brush brush = new SolidBrush(ColorDefiner.DefineColor(tag.Frequency));
                    graphics.DrawString(tag.Word, new Font(FontName, tag.Size), brush, tag.LocationRectangle.Location);
                }
            }

            return bitmap;
        }

        private static Rectangle GetImageRectangle(IEnumerable<Tag.Tag> tags)
        {
            var rectangles = tags.Select(t => t.LocationRectangle);
            var enumerable = rectangles as Rectangle[] ?? rectangles.ToArray();
            var minX = enumerable.Min(rect => rect.Left);
            var minY = enumerable.Min(rect => rect.Top);
            var maxX = enumerable.Max(rect => rect.Right);
            var maxY = enumerable.Max(rect => rect.Bottom);
            return new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY));
        }
        
    }
}