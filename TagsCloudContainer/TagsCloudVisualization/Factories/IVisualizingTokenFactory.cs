using System.Drawing;
using TagsCloudVisualization.Visualization.Configurator;

namespace TagsCloudVisualization.Factories
{
    public interface IVisualizingTokenFactory
    {
        public IVisualizingToken NewToken(string value, int fontSize, Color color);
    }
}