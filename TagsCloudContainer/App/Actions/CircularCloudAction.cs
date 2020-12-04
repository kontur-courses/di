using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    internal class CircularCloudAction : IUiAction
    {
        private readonly ICloudVisualizer circularCloudVisualizer;
        private readonly PictureBoxImageHolder imageHolder;

        public CircularCloudAction(ICloudVisualizer circularCloudVisualizer, PictureBoxImageHolder imageHolder)
        {
            this.circularCloudVisualizer = circularCloudVisualizer;
            this.imageHolder = imageHolder;
        }

        public MenuCategory Category => MenuCategory.Algorithms;
        public string Name => CloudLayouterAlgorithm.CircularCloudLayouter.GetDescription();
        public string Description => CloudLayouterAlgorithm.CircularCloudLayouter.GetDescription();

        public void Perform()
        {
            imageHolder.GetAppSettings().LayouterAlgorithm = CloudLayouterAlgorithm.CircularCloudLayouter;
            circularCloudVisualizer.Visualize(imageHolder.GetAppSettings());
        }
    }
}