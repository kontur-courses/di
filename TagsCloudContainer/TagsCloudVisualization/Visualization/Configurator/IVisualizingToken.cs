using System.Drawing;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public interface IVisualizingToken<out T>
    {
        public T Value { get; }

        public SizeF RectangleSize { get; }

        public Color Color { get; }
    }
}