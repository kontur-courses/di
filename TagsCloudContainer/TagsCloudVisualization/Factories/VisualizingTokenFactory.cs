using System.Drawing;
using TagsCloudVisualization.Visualization.Configurator;

namespace TagsCloudVisualization.Factories
{
    public class WordsVisualizingTokenFactory : IVisualizingTokenFactory
    {
        public IVisualizingToken NewToken(string value, int fontSize, Color color)
        {
            return new WordVisualizingToken(value, fontSize, color);
        }
    }
}