using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    internal class CircularCloudAction : IUiAction
    {
        private readonly ICloudVisualizer circularCloudVisualizer;
        private readonly LayouterAlgorithmSettings settings;

        public CircularCloudAction(ICloudVisualizer circularCloudVisualizer, LayouterAlgorithmSettings settings)
        {
            this.circularCloudVisualizer = circularCloudVisualizer;
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.Algorithms;
        public string Name => CloudLayouterAlgorithm.CircularCloudLayouter.GetDescription();
        public string Description => CloudLayouterAlgorithm.CircularCloudLayouter.GetDescription();

        public void Perform()
        {
            settings.LayouterAlgorithm = CloudLayouterAlgorithm.CircularCloudLayouter;
            circularCloudVisualizer.Visualize();
        }
    }
}