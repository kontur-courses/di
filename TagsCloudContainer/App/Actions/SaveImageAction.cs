using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly AppSettings appSettings;
        private readonly IImageHolder imageHolder;

        public SaveImageAction(IImageHolder imageHolder, AppSettings appSettings)
        {
            this.imageHolder = imageHolder;
            this.appSettings = appSettings;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(appSettings.OutputDirectory),
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Изображения (*.png)|*.png"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.SaveImage(dialog.FileName);
        }
    }
}