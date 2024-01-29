using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class SelectSourceFileAction : IUiAction
    {
        private FileSettings fileSettings;

        public SelectSourceFileAction(FileSettings settings)
        {
            this.fileSettings = settings;
        }

        public string Category => "Файлы";

        public string Name => "Файл cо словами";

        public string Description => "Выбрать файл со словами для облака тегов";

        public void Perform()
        {
            var filePath = fileSettings.SourceFilePath;
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                InitialDirectory = Path.GetFullPath(filePath),
                DefaultExt = "txt",
                FileName = "source.txt",
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };
            var res = dialog.ShowDialog();

            if (res == DialogResult.OK)
                filePath = dialog.FileName;

            fileSettings.SourceFilePath = filePath;
        }
    }
}