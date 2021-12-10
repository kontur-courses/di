using System.Drawing;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public interface IVisualizingToken
    {
        public string Value { get; }

        public int FontSize { get; }

        public Color Color { get; }
    }
}