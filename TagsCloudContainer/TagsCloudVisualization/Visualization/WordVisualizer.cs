using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Visualization.Configurator;

namespace TagsCloudVisualization.Visualization
{
    public class WordVisualizer : IVisualizer
    {
        private readonly IVisualizingConfigurator configurator;
        private readonly ILayouter layouter;

        public WordVisualizer(ILayouter layouter, IVisualizingConfigurator configurator)
        {
            this.layouter = layouter;
            this.configurator = configurator;
        }

        public Bitmap Visualize(IEnumerable<string> visualizingValues)
        {
            throw new System.NotImplementedException();
        }
    }
}