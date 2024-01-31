using TagsCloudContainer.Infrastucture.Extensions;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private ImageSettings imageSettings;
        private PictureBox pictureBox;

        public ImageSettingsAction(ImageSettings settings, PictureBox pictureBox)
        {
            this.imageSettings = settings;
            this.pictureBox = pictureBox;
        }

        public string Category => "Настроить";

        public string Name => "Изображение";

        public string Description => "Изменить настройки изображения";

        public void Perform()
        {
            SettingsForm.For(imageSettings).ShowDialog();
            pictureBox.RecreateImage(imageSettings);
        }
    }
}