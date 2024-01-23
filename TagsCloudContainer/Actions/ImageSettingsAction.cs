using TagsCloudContainer.Client;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private ImageSettings imageSettings;
        private ITagCloudClient tagCloudClient;

        public ImageSettingsAction(ImageSettings settings, ITagCloudClient tagCloudClient)
        { 
            this.imageSettings = settings;  
            this.tagCloudClient = tagCloudClient;
        }

        public string Category => "Настроить";

        public string Name => "Изображение";

        public string Description => "Изменить настройки изображения";

        public void Perform()
        {
            tagCloudClient.SetSettings(imageSettings);
        }
    }
}