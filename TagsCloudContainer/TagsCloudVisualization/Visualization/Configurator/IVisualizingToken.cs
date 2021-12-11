using System.Drawing;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public interface IVisualizingToken
    {
        public string Value { get; }

        public Font Font { get; }

        public Color Color { get; }
    }
}