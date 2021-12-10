using System.Collections.Generic;
using TagsCloudVisualization.Factories;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public class WordsVisualizingConfigurator : IVisualizingConfigurator
    {
        private readonly IVisualizingTokenFactory tokenFactory;

        public WordsVisualizingConfigurator(IVisualizingTokenFactory tokenFactory)
        {
            this.tokenFactory = tokenFactory;
        }
        
        public IEnumerable<IVisualizingToken> Configure(IEnumerable<string> visualizingValues)
        {
            throw new System.NotImplementedException();
        }
    }
}