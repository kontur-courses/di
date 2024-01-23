using TagsCloudContainer.Client;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class SaveImageAction : IUiAction
    {
        private FileSettings fileSettings;
        private ITagCloudClient tagCloudClient;

        public SaveImageAction(FileSettings settings, ITagCloudClient tagCloudClient)
        { 
            this.fileSettings = settings;  
            this.tagCloudClient = tagCloudClient;
        }

        public string Category => "�����������";

        public string Name => "���������";

        public string Description => "��������� �����������";

        public void Perform()
        {
            tagCloudClient.SaveImage(fileSettings.ImagePath);
        }
    }
}