using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.Canvases;

namespace TagsCloudVisualization.FormAction
{
    public class ImageSettingsAction : IFormAction
    {
        public string Category => "Settings";
        public string Name => "Image size";
        public string Description => "Change image size of your tag cloud visualization";

        private readonly ImageSettings imageSettings;
        private readonly ICanvas canvas;
        
        public ImageSettingsAction(ImageSettings imageSettings, ICanvas canvas)
        {
            this.imageSettings = imageSettings;
            this.canvas = canvas;
        }

        public void Perform()
        {
            SettingsForm.For(imageSettings).ShowDialog();
            canvas.RecreateImage(imageSettings);
        }
    }
}