using System.IO;
using System.Windows.Forms;
using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IImageDirectoryProvider imageDirectoryProvider;
        private readonly IImageHolder imageHolder;

        public SaveImageAction(IImageHolder imageHolder, IImageDirectoryProvider imageDirectoryProvider)
        {
            this.imageHolder = imageHolder;
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
                InitialDirectory = Path.GetFullPath(imageDirectoryProvider.ImagesDirectory),
                DefaultExt = "",
                FileName = "image.png",
                Filter = "Изображения (*.png)|*.png|All files(*.*)|*.*"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.SaveImage(dialog.FileName);
        }
    }
}