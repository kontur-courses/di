using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
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
                
                using var rBrush = new SolidBrush(imageSettings.RectangleBackgroundColor);
                using var rBorderPen = new Pen(imageSettings.RectangleBordersColor);
                foreach (var pair in rectangles)
                {
                    g.FillRectangle(rBrush, pair.rectangle);
                    g.DrawRectangle(rBorderPen, pair.rectangle);
                    using var font = GetAdjustedFont(g, pair.text, imageSettings.Font, 
                        pair.rectangle.Width, 100, 1);
                    g.DrawString(pair.text, font, tBrush, pair.rectangle);
                }
            }
            pictureBox.UpdateUi();
        }

        public Font GetAdjustedFont(Graphics g, string graphicString, Font originalFont, int containerWidth, int maxFontSize, int minFontSize)
        {
            Font testFont = null;     
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);
                
                SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont);

                if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width))
                {
                    return testFont;
                }
            }

            return originalFont;
        }
    }
}
