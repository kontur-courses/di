using System.Drawing;

namespace TagsCloudVisualization
{
    public class WordParameter
    {
        public RectangleF WordRectangleF { get; }
        public string Word { get; }
        public SizeF Size => WordRectangleF.Size;
        public PointF Location => WordRectangleF.Location;
        public Font Font { get; }

        public WordParameter(string word, RectangleF rectangleF, Font font)
        {
            WordRectangleF = rectangleF;
            Font = font;
            Word = word;
        }
    }
}