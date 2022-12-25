using TagCloudGUI.Interfaces;

namespace TagCloudGUI.Actions
{
    internal class SourceTagsAction : IActionForm
    {
        private IAlgorithmSettings settings;
        public SourceTagsAction(IAlgorithmSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Настройки";
        public string Name => "Загрузить данные";
        public string Description => "Выбрать текстовый файл со словами, из которых нужно получить облако";

        public void Perform()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text files(*.txt)|*.txt|Doc Files(*.doc)|*.doc)|Docx Files(*.docx)|*.docx)";
            dialog.ShowDialog();
            settings.ImagesDirectory = dialog.FileName;
        }
    }
}
