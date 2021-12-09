using System.Drawing;
using TagsCloudVisualization.Visualization.Configurator;

namespace TagsCloudVisualization.Factories
{
    public class WordsVisualizingTokenFactory : IVisualizingTokenFactory<string>
    {
        public IVisualizingToken<string> NewToken(string value, SizeF rectangleSize, Color color)
        {
            return new WordVisualizingToken(value, rectangleSize, color);
        }
    }
}