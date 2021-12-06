using TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer
{
    public class CloudVisualizator
    {
        private readonly IVisualizator<ITag> visualizator;
        private readonly IVisualizatorSettings settings;

        public CloudVisualizator(IVisualizator<ITag> visualizator, IVisualizatorSettings settings)
        {
            this.visualizator = visualizator;
            this.settings = settings;
        }

        public void Visualize(ICloud<ITag> cloud) 
            => visualizator.Visualize(settings, cloud);
    }
}