using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.Visualisator
{
    public class TagCloudPainter
    {
        private CircularCloudLayouter cloudLayouter;
        ImageHolder imageHolder;
        ImageSettings imageSettings;


        public TagCloudPainter(CircularCloudLayouter cloudLayouter, ImageHolder imageHolder, ImageSettings imageSettings)
        {
            this.cloudLayouter = cloudLayouter;
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
        }

        public void Paint()
        {
            var rectangles = cloudLayouter.FindRectanglesPositions();
            using (var g = imageHolder.StartDrawing())
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
            imageHolder.UpdateUi();
        }
    }
}
