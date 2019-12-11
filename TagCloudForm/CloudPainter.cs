using TagCloud.Visualization;
using TagCloudForm.Holder;

namespace TagCloudForm
{
    public class CloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;
        private readonly CloudVisualization cloudVisualization;

        public CloudPainter(IImageHolder imageHolder,
            ImageSettings imageSettings, CloudVisualization cloudVisualization)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
            this.cloudVisualization = cloudVisualization;
        }

        public void Paint()
        {
            imageHolder.RecreateImage(imageSettings);
            imageHolder.Image = cloudVisualization.Visualize();
            imageHolder.UpdateUi();
        }
    }
}