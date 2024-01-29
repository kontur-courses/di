using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class SelectBoringWordsFileAction : IUiAction
    {
        private FileSettings fileSetting;

        public SelectBoringWordsFileAction(FileSettings settings)
        {
            this.fileSetting = settings;
        }

        public string Category => "Файлы";

        public string Name => "Файл со скучными словами";

        public string Description => "Выбрать файл со списком скучных слов";

        public void Perform()
        {
            var filePath = fileSetting.BoringFilePath;
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                InitialDirectory = Path.GetFullPath(filePath),
                DefaultExt = "txt",
                FileName = "boring.txt",
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };
            var res = dialog.ShowDialog();

            if (res == DialogResult.OK)
                filePath = dialog.FileName;

            fileSetting.BoringFilePath = filePath;
        }
    }
}