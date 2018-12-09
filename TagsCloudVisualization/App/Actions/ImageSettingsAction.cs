using TagsCloudVisualization.TagsCloud;

namespace TagsCloudVisualization.App.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        public string Name => "Настройки изображения";
        public string Category => "Настройки";
        private readonly ImageSettings imageSettings;

        public ImageSettingsAction(ImageSettings imageSettings)
        {
            this.imageSettings = imageSettings;
        }
        public void Perform()
        {
            SettingsForm.For(imageSettings).ShowDialog();
        }
    }
}