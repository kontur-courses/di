using System.IO;
using System.Windows.Forms;

namespace GUITagClouder
{
    public class NewFileAction : IGuiAction
    {
        public NewFileAction(CloudHolder imageHolder, IPathProvider provider)
        {
            this.imageHolder = imageHolder;
            this.provider = provider;
        }
        private readonly CloudHolder imageHolder;
        private readonly IPathProvider provider;

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
            provider.SourcePath = dialog.FileName;
            imageHolder.RecreateImage();

        }
    }
}