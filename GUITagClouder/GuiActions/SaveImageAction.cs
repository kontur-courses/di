using System.IO;
using System.Windows.Forms;

namespace GUITagClouder
{
    public class SaveImageAction : IGuiAction
    {
        
        public SaveImageAction(CloudProcessor imageProcessor)
        {
            this.imageProcessor = imageProcessor;
        }
        private readonly CloudProcessor imageProcessor;

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
            imageProcessor.SaveImage(dialog.FileName);
        }
    }
}