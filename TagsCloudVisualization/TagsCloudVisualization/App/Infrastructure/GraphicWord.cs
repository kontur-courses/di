using System.Drawing;

namespace TagsCloudVisualization
{
    public class GraphicWord
    {
        public string Value { get; set; }
        public int Rate { get; set; }
        public Rectangle Rectangle { get; set; }
        public Color Color { get; set; }
        public Font Font { get; set; }

        public GraphicWord(string value)
        {
            Value = value;
        }
    }
}