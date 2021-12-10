using System.Drawing;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public interface IVisualizingToken
    {
        public string Value { get; }

        public SizeF RectangleSize { get; }

        public Color Color { get; }
    }
}