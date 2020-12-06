using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    internal class CircularCloudAction : IUiAction
    {
        private readonly ICloudVisualizer circularCloudVisualizer;
        private readonly AppSettings appSettings;

        public CircularCloudAction(ICloudVisualizer circularCloudVisualizer, AppSettings appSettings)
        {
            this.circularCloudVisualizer = circularCloudVisualizer;
            this.appSettings = appSettings;
        }

        public MenuCategory Category => MenuCategory.Algorithms;
        public string Name => CloudLayouterAlgorithm.CircularCloudLayouter.GetDescription();
        public string Description => CloudLayouterAlgorithm.CircularCloudLayouter.GetDescription();

        public void Perform()
        {
            appSettings.LayouterAlgorithm = CloudLayouterAlgorithm.CircularCloudLayouter;
            circularCloudVisualizer.Visualize();
        }
    }
}