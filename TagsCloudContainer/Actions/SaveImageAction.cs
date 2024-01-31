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
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(fileSettings.ImagePath),
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Изображения (*.png)|*.png|Изображения (*.jpg)|*.jpg|Изображения (*.bmp)|*.bmp"
            };
            var res = dialog.ShowDialog();

            if (res == DialogResult.OK)
                tagCloudClient.SaveImage(dialog.FileName);
        }
    }
}