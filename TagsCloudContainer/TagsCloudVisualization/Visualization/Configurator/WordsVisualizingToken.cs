using System.Drawing;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public class WordVisualizingToken : IVisualizingToken
    {
        public string Value { get; }
        public int FontSize { get; }
        public Color Color { get; }
        
        public WordVisualizingToken(string value, int fontSize, Color color)
        {
            Value = value;
            FontSize = fontSize;
            Color = color;
        }
    }
}