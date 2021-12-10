using System.Drawing;
using TagsCloudVisualization.Visualization.Configurator;

namespace TagsCloudVisualization.Factories
{
    public class WordsVisualizingTokenFactory : IVisualizingTokenFactory
    {
        public IVisualizingToken NewToken(string value, SizeF rectangleSize, Color color)
        {
            return new WordVisualizingToken(value, rectangleSize, color);
        }
    }
}