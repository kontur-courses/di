using TagsCloudVisualization.InterfacesForSettings;
using TagsCloudVisualization.TagsCloud;

namespace TagsCloudVisualization.App.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        public string Name => "Настройки изображения";
        public string Category => "Настройки";
        private readonly IImageSettings imageSettings;

        public ImageSettingsAction(IImageSettings imageSettings)
        {
            this.imageSettings = imageSettings;
        }
        public void Perform()
        {
            SettingsForm.For(imageSettings).ShowDialog();
        }
    }
}