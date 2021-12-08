using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Visualization.Configurator;

namespace TagsCloudVisualization.Visualization
{
    public class WordVisualizer : IVisualizer<string>
    {
        private readonly IVisualizingConfigurator<string> configurator;
        private readonly ILayouter layouter;

        public WordVisualizer(ILayouter layouter, IVisualizingConfigurator<string> configurator)
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