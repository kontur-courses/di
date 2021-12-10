using System.Collections.Generic;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public interface IVisualizingConfigurator
    {
        public IEnumerable<IVisualizingToken> Configure(IEnumerable<string> visualizingValues);
    }
}