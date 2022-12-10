using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Visualisator
{
    public class TagCloudPainter : IPainter
    {
        PictureBox pictureBox;
        ImageSettings imageSettings;

        public TagCloudPainter(PictureBox pictureBox, ImageSettings imageSettings)
        {
            this.pictureBox = pictureBox;
            this.imageSettings = imageSettings;
        }

        public void Paint(List<(Rectangle rectangle, string text)> rectangles)
        {
            using (var g = pictureBox.StartDrawing())
            {
                g.Clear(imageSettings.BackgroundColor);
                using var tBrush = new SolidBrush(imageSettings.TextColor);
                using var font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
                using var rBrush = new SolidBrush(imageSettings.RectangleBackgroundColor);
                using var rBorderPen = new Pen(imageSettings.RectangleBordersColor);
                foreach (var pair in rectangles)
                {
                    g.FillRectangle(rBrush, pair.rectangle);
                    g.DrawString(pair.text, font, tBrush, pair.rectangle);
                    g.DrawRectangle(rBorderPen, pair.rectangle);
                }
            }
            pictureBox.UpdateUi();
        }
    }
}
