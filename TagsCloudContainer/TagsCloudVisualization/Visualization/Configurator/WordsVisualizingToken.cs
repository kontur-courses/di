using System.Drawing;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public class WordVisualizingToken : IVisualizingToken
    {
        public string Value { get; }
        public Font Font { get; }
        public Color Color { get; }
        
        public WordVisualizingToken(string value, Font font, Color color)
        {
            Value = value;
            Font = font;
            Color = color;
        }
    }
}