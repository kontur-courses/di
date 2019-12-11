using System.IO;
using System.Windows.Forms;
using TagCloudForm.Holder;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IDirectoryProvider directoryProvider;
        private readonly IImageHolder imageHolder;

        public SaveImageAction(IDirectoryProvider directoryProvider, IImageHolder imageHolder)
        {
            this.directoryProvider = directoryProvider;
            this.imageHolder = imageHolder;
        }

        public string Category => "Файл";
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(directoryProvider.Directory),
                DefaultExt = "png",
                FileName = "image",
                Filter = "Изображения (*.png)|*.png"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.SaveImage(dialog.FileName);
        }
    }
}