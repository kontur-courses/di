using System.Drawing;
using Point = TagCloud.Layouter.Point;
using Size = TagCloud.Layouter.Size;

namespace TagCloud.Visualizer
{
    public class VisualElement
    {
        public VisualElement(
            PositionedElement element,
            Color color)
        {
            Word = element.Word;
            Position = element.Rectangle.Center;
            Color = color;
            Size = element.Rectangle.Size;
            Font = element.Font;
            Frequency = element.Frequency;
        }

        public VisualElement(
            string word,
            Point position,
            Size size,
            Color color,
            Font font,
            int frequency)
        {
            Word = word;
            Position = position;
            Color = color;
            Size = size;
            Font = font;
            Frequency = frequency;
        }

        public Point Position { get; set; }
        public Color Color { get; }
        public string Word { get; }
        public Size Size { get; }
        public Font Font { get; }
        public int Frequency { get; }
    }
}