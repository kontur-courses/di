using TagsCloudContainer.Client;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class SelectBoringWordsFileAction : IUiAction
    {
        private FileSettings fileSetting;
        private ITagCloudClient tagCloudClient;

        public SelectBoringWordsFileAction(FileSettings settings, ITagCloudClient tagCloudClient)
        { 
            this.fileSetting = settings;  
            this.tagCloudClient = tagCloudClient;
        }

        public string Category => "Файлы";

        public string Name => "Файл со скучными словами";

        public string Description => "Выбрать файл со списком скучных слов";

        public void Perform()
        {
            fileSetting.BoringFilePath = tagCloudClient.SetBoringFilePath(fileSetting.BoringFilePath);
        }
    }
}