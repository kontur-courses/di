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

        public string Category => "Изображение";

        public string Name => "Сохранить";

        public string Description => "Сохранить изображение";

        public void Perform()
        {
            tagCloudClient.SaveImage(fileSettings.ImagePath);
        }
    }
}