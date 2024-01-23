using TagsCloudContainer.Client;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class SelectSourceFileAction : IUiAction
    {
        private FileSettings fileSettings;
        private ITagCloudClient tagCloudClient;

        public SelectSourceFileAction(FileSettings settings, ITagCloudClient tagCloudClient)
        { 
            this.fileSettings = settings;  
            this.tagCloudClient = tagCloudClient;
        }

        public string Category => "�����";

        public string Name => "���� c� �������";

        public string Description => "������� ���� �� ������� ��� ������ �����";

        public void Perform()
        {
            fileSettings.SourceFilePath = tagCloudClient.SetSourceFilePath(fileSettings.SourceFilePath);
        }
    }
}