using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    public class Visualizer : IVisualizer
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private ICloudSetting CloudSetting { get; }

        public Visualizer(ICloudSetting cloudSetting)
        {
            CloudSetting = cloudSetting;
            bitmap = new Bitmap(cloudSetting.ImageSize.Width, cloudSetting.ImageSize.Height);
            graphics = Graphics.FromImage(bitmap);
        }

        public void Clear()
        {
            graphics.Clear(CloudSetting.BackgroundColor);
        }

        public void DrawTag(TagRectangle tag, Font font)
        {
            var brash = new SolidBrush(CloudSetting.TextColor);
            graphics.DrawString(tag.Value, font, brash, tag.Rectangle.Location);
        }
        
        public void DrawRectangles(IEnumerable<Rectangle> rectangles)
        {
            var pen = new Pen(CloudSetting.BackgroundColor, 2);
            var brash = new SolidBrush(CloudSetting.TextColor);
            foreach (var rectangle in rectangles)
            {
                graphics.FillRectangle(brash, rectangle);
                graphics.DrawRectangle(pen, rectangle);
            }
        }

        public Bitmap Save()
        {
            return bitmap;
        }
    }
}