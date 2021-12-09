using System.Drawing;
using TagsCloudVisualization.Visualization.Configurator;

namespace TagsCloudVisualization.Factories
{
    public interface IVisualizingTokenFactory<T>
    {
        public IVisualizingToken<T> NewToken(T value, SizeF rectangleSize, Color color);
    }
}