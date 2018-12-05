using System.Drawing;
using Point = TagsCloudVisualization.Layouter.Point;
using Size = TagsCloudVisualization.Layouter.Size;

namespace TagsCloudVisualization.Visualizer
{
    public class VisualElement
    {
        public Point Position { get; }
        public Color Color { get; }
        public string Word { get; }
        public Size Size { get; }
        public Font Font { get; }
        public int Frequency { get; }

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
    }
}