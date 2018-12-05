using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class MonochromePalette : IWordPalette
    {
        private readonly Color backColor;
        private readonly Color wordColor;
        private readonly Font Font;

        public MonochromePalette(Font font, Color wordColor, Color backColor)
        {
            this.backColor = backColor;
            this.wordColor = wordColor;
            Font = font;
        }

        public Image GetBackground(Size size)
        {
            var backImage = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(backImage);
            graphics.FillRectangle(new SolidBrush(backColor), 0, 0, size.Width, size.Height);
            return backImage;
        }

        public void ColorWords(IEnumerable<GraphicWord> words)
        {
            foreach (var graphicWord in words)
            {
                graphicWord.Color = wordColor;
                graphicWord.Font = new Font(Font.Name, graphicWord.Rectangle.Height);
            }
        }
    }
}
