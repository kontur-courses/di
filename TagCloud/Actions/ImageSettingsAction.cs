using TagCloud;
using TagCloud.Settings;

namespace TagsCloudApp.Actions
{
    public class ImageSettingAction : IUiAction
    {
        private readonly ImageBox imageBox;
        private readonly ImageSettings imageSettings;
        public ImageSettingAction(ImageBox imageBox, ImageSettings imageSettings)
        {
            this.imageBox = imageBox;
            this.imageSettings = imageSettings;
        }
        public string Category => "Settings";
        public string Name => "Image";
        public string Description => "Change Image Settings";
        public void Perform()
        {
            SettingsForm.For(imageSettings).ShowDialog();
            imageBox.RecreateImage(imageSettings);
        }
    }

}