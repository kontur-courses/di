using TagsCloudContainer.Client;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class AlgorithmSettingsAction : IUiAction
    {
        private AlgorithmSettings algorithmSettings;
        private ITagCloudClient tagCloudClient;

        public AlgorithmSettingsAction(AlgorithmSettings settings, ITagCloudClient tagCloudClient)
        { 
            this.algorithmSettings = settings;  
            this.tagCloudClient = tagCloudClient;
        }

        public string Category => "���������";

        public string Name => "��������";

        public string Description => "�������� ��������� ���������";

        public void Perform()
        {
            tagCloudClient.SetSettings(algorithmSettings);
        }
    }
}