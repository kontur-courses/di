using System.IO;
using System.Windows.Forms;

namespace GUITagClouder
{
    public class NewFileAction : IGuiAction
    {
        public NewFileAction(CloudProcessor imageProcessor)
        {
            this.imageProcessor = imageProcessor;
        }
        private readonly CloudProcessor imageProcessor;

        public string Category => "Файл";
        public string Name => "Открыть";
        public string Description => "Открыть новый файл.";

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Directory.GetCurrentDirectory(),
                DefaultExt = ".png"
            };
            var res = dialog.ShowDialog();
            if (res != DialogResult.OK) return;
            
            imageProcessor.SourcePath = dialog.FileName;
            imageProcessor.RecreateImage();
        }
    }
}