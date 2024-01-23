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

        public string Category => "Файлы";

        public string Name => "Файл cо словами";

        public string Description => "Выбрать файл со словами для облака тегов";

        public void Perform()
        {
            fileSettings.SourceFilePath = tagCloudClient.SetSourceFilePath(fileSettings.SourceFilePath);
        }
    }
}