using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface.Actions
{
    internal class SourceTagsAction : IActionForm
    {
        private IAlgorithmSettings settings;
        public SourceTagsAction(IAlgorithmSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Настройки";
        public string Name => "Выбрать файл с тегами";
        public string Description => "Теги для облака";

        public void Perform()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text files(*.txt)|*.txt|Doc Files(*.doc)|*.doc)|Docx Files(*.docx)|*.docx)";
            dialog.ShowDialog();
            settings.ImagesDirectory = dialog.FileName;
        }
    }
}
