using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class MonochromePalette : IWordPalette
    {
        public Color BackColor { get; set; } = Color.White;
        public Color WordColor { get; set; } = Color.Black;

        public MonochromePalette()
        {

        }

        public MonochromePalette(Color wordColor, Color backColor)
        {
            BackColor = backColor;
            WordColor = wordColor;
        }

        public Image GetBackground(Size size)
        {
            var backImage = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(backImage);
            graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, size.Width, size.Height);
            return backImage;
        }

        public void ColorWords(IEnumerable<GraphicWord> words)
        {
            foreach (var graphicWord in words)
            {
                graphicWord.Color = WordColor;
            }
        }
    }
}
