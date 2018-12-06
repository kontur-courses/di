using System.Windows.Forms;

namespace TagsCloudVisualization.App.Actions
{
    public class SaveImageAction : IUiAction
    {
        public string Name => "Сохранить картинку как...";
        public string Category => "Файл";
        private readonly PictureBoxImageHolder imageHolder;
        public SaveImageAction(PictureBoxImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }
        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                Title = "Сохранить картинку как...",
                OverwritePrompt = true,
                CheckPathExists = true,
                DefaultExt = "png",
                FileName = "TagCloud.png",
                Filter = "Изображения (*.png)|*.png",
                ShowHelp = true
            };
            if (dialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                imageHolder.SaveImage(dialog.FileName);
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}