using System.IO;
using System.Windows.Forms;

namespace GUITagClouder
{
    public class SaveImageAction : IGuiAction
    {
        
        public SaveImageAction(IImageHolder imageHolder, IPathProvider provider)
        {
            this.imageHolder = imageHolder;
            this.provider = provider;
        }
        private readonly IImageHolder imageHolder;
        private readonly IPathProvider provider;

        public string Category => "Файл";
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                DefaultExt = ".png"
            };
            var res = dialog.ShowDialog();
            if (res != DialogResult.OK) return;
            provider.TargetPath = dialog.FileName;
            imageHolder.SaveImage();
        }
    }
}