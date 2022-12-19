using TagCloudGUI.Interfaces;

namespace TagCloudGUI.Actions
{
    public class SaveAction : IActionForm
    {
        private IImageSettingsProvider imageDirectoryProvider;

        public SaveAction(IImageSettingsProvider imageDirectoryProvider)
        {
            this.imageDirectoryProvider = imageDirectoryProvider;
        }

        public string Category => "Файл";
        public string Name => "Сохранить как...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(Environment.CurrentDirectory),
                DefaultExt = "png",
                FileName = "image.png",
                Filter =
                    "JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png"
            };

            var res = dialog.ShowDialog();

            if (res == DialogResult.OK)
                imageDirectoryProvider.SaveImage(dialog.FileName);
        }
    }
}
