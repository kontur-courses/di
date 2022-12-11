using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface.Actions
{
    public class SaveAction : IActionForm
    {
        private IImageSettingsProvider imageDirectoryProvider;

        public SaveAction(IImageSettingsProvider imageDirectoryProvider)
        {
            this.imageDirectoryProvider = imageDirectoryProvider;
        }

        public string Category => "Файл";
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(Environment.CurrentDirectory),
                DefaultExt = "bmp",
                FileName = "image.bmp",
                Filter =
                    "Bitmap Image (.bmp)|*.bmp|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageDirectoryProvider.SaveImage(dialog.FileName);
        }
    }
}
