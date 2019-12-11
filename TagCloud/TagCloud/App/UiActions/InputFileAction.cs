using System.Windows.Forms;

namespace TagCloud
{
    public class InputFileAction : IUiAction
    {
        private readonly Reader reader;

        public InputFileAction(Reader reader)
        {
            this.reader = reader;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Read from file";
        public string Description => "Read words from selected file";

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = HelperMethods.GetProjectDirectory()
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                reader.PathToFile = dialog.FileName;
        }
    }
}
