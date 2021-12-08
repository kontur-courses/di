using System.Collections;
using System.Collections.Generic;

namespace TagsCloudVisualization.Visualization.Configurator
{
    public interface IVisualizingConfigurator<T>
    {
        public IEnumerable<IVisualizingToken<T>> Configure(IEnumerable<T> visualizingValues);
    }
}