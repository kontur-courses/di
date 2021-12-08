using System.Drawing;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public class WordVisualizingToken : IVisualizingToken<string>
    {
        public string Value { get; }
        public SizeF RectangleSize { get; }
        public Color Color { get; }
        
        public WordVisualizingToken(string value, SizeF rectangleSize, Color color)
        {
            Value = value;
            RectangleSize = rectangleSize;
            Color = color;
        }
    }
}