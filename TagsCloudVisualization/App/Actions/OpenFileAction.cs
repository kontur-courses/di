using System.Windows.Forms;
using TagsCloudVisualization.InterfacesForSettings;
using TagsCloudVisualization.TagsCloud;

namespace TagsCloudVisualization.App.Actions
{
    public class OpenFileAction : IUiAction
    {
        public string Name => "Открыть";

        public string Category => "Файл";
        private readonly ITagsCloudSettings tagCloudSettings;

        public OpenFileAction(ITagsCloudSettings tagCloudSettings)
        {
            this.tagCloudSettings = tagCloudSettings;
        }

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                Filter = @"Text files(*.txt;*.doc;*.docx)|*.txt;*.doc;*.docx|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                tagCloudSettings.WordsSettings.PathToFile = dialog.FileName;
            }
            catch
            {
                MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}