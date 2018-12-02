using System.IO;
using System.Windows.Forms;
using CloudLayouter.Infrastructer;
using CloudLayouter.Infrastructer.Common;

namespace CloudLayouter.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IImageDirectoryProvider imageDirectoryProvider;
        private readonly IImageHolder imageHolder;

        public SaveImageAction(IImageDirectoryProvider imageDirectoryProvider, IImageHolder imageHolder)
        {
            this.imageDirectoryProvider = imageDirectoryProvider;
            this.imageHolder = imageHolder;
        }

        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(imageDirectoryProvider.ImagesDirectory),
                DefaultExt = "bmp",
                FileName = "image.bmp",
                Filter = "Изображения (*.bmp)|*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.SaveImage(dialog.FileName);
        }

        public MenuCategory Category => MenuCategory.SaveFile;
    }
}