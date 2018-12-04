using System.Drawing;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public Color Color { get; }
        public string Value { get; }
        public int Frequency { get; }

        public Tag(string value, int frequency, Color color)
        {
            Value = value;
            Color = color;
            Frequency = frequency;
        }
    }
}