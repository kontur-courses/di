using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace TagsCloudVisualization
{
    public class GradientPalette : IWordPalette
    {
        public Color HotColor { get; set; }
        public Color ColdColor { get; set; }
        public Color BackColor { get; set; }

        public GradientPalette(Color hotColor, Color coldColor, Color backColor)
        {
            HotColor = hotColor;
            ColdColor = coldColor;
            BackColor = backColor;
        }

        public Image GetBackground(Size size)
        {
            var backImage = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(backImage);
            graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, size.Width, size.Height);
            var path = new GraphicsPath();
            path.AddEllipse(0, 0, size.Width, size.Height);
            var brush = new PathGradientBrush(path);
            brush.CenterColor = Color.FromArgb(100, HotColor.R, HotColor.G, HotColor.B);
            brush.SurroundColors = new [] { BackColor };
            graphics.FillEllipse(brush, 0, 0, size.Width, size.Height);
            return backImage;
        }

        public void ColorWords(IEnumerable<GraphicWord> words)
        {
            double maxRate = words.First().Rate;
            foreach (var graphicWord in words)
            {
                var coef = graphicWord.Rate / maxRate;
                graphicWord.Color = Color.FromArgb(
                    (int)(HotColor.R * coef + ColdColor.R * (1 - coef)),
                    (int)(HotColor.G * coef + ColdColor.G * (1 - coef)),
                    (int)(HotColor.B * coef + ColdColor.B * (1 - coef)));
            }
        }
    }
}
