using TagsCloudContainer.Client;
using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class DrawTagCloudAction : IUiAction
    {
        private ImageSettings imageSettings;
        private FileSettings fileSettings;
        private ITagCloudClient tagCloudClient;

        public DrawTagCloudAction(ImageSettings imageSettings, FileSettings fileSettings, ITagCloudClient tagCloudClient)
        { 
            this.tagCloudClient = tagCloudClient;
            this.imageSettings = imageSettings;
            this.fileSettings = fileSettings;
        }

        public string Category => "Изображение";

        public string Name => "Отрисовать изображение";

        public string Description => "Отрисовать изображение облака тегов";

        public void Perform()
        {
            tagCloudClient.DrawImage(fileSettings.SourceFilePath,
                fileSettings.BoringFilePath, imageSettings.Width, imageSettings.Height);
        }
    }
}