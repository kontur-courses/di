using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.UI.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IImageDirectoryProvider imageDirectory;
        private readonly IImageHolder image;

        public SaveImageAction(IImageDirectoryProvider imageDirectory, IImageHolder image)
        {
            this.imageDirectory = imageDirectory;
            this.image = image;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Сохранить";
        public string Description => "Сохранить Изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(imageDirectory.ImagesDirectory),
                DefaultExt = "bmp",
                FileName = "image.bmp",
                Filter = "Изображения    (*.bmp)|*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                image.SaveImage(dialog.FileName);
        }
    }
}