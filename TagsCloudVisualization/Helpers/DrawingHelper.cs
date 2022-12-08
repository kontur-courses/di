using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Configurations;

namespace TagsCloudVisualization.Helpers
{
    public static class DrawingHelper
    {
        public static Bitmap DrawTagCloud(Dictionary<Tag, Rectangle> tags, CloudConfiguration cloudConfiguration)
        {
            var imageWidth = cloudConfiguration.ImageSize.Width;
            var imageHeight = cloudConfiguration.ImageSize.Height;

            var bitmap = new Bitmap(imageWidth, imageHeight);
            using var gp = Graphics.FromImage(bitmap);
            
            gp.FillRectangle(new SolidBrush(cloudConfiguration.BackgroundColor), new Rectangle(0,0, imageWidth, imageHeight));

            foreach (var tag in tags)
                DrawStringInside(gp, tag.Value, new Font(cloudConfiguration.FontFamily, 1), new SolidBrush(cloudConfiguration.PrimaryColor), tag.Key.Text);

            return bitmap;
        }

        private static void DrawStringInside(Graphics graphics, Rectangle rect, Font font, Brush brush, string text)
        {
            var textSize = graphics.MeasureString(text, font);
            var state = graphics.Save();
            graphics.TranslateTransform(rect.Left, rect.Top);
            graphics.ScaleTransform(rect.Width / textSize.Width, rect.Height / textSize.Height);
            graphics.DrawString(text, font, brush, PointF.Empty);
            graphics.Restore(state);
        }
    }
}