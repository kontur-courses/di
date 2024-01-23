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

        public string Category => "���������";

        public string Name => "�����������";

        public string Description => "�������� ��������� �����������";

        public void Perform()
        {
            tagCloudClient.SetSettings(imageSettings);
        }
    }
}