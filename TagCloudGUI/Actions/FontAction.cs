using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface.Actions
{
    internal class FontAction : IActionForm
    {
        private IAlgorithmSettings settings;
        public FontAction(IAlgorithmSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Настройки";
        public string Name => "Выбрать шрифт";
        public string Description => "Шрифт для слов облака";

        public void Perform()
        {
            var fontDialog = new FontDialog();
            fontDialog.ShowEffects = false;
            fontDialog.ShowApply = false;
            fontDialog.MinSize = 20;
            fontDialog.MaxSize = 20;
            fontDialog.ShowDialog();
            settings.Font = fontDialog.Font.FontFamily;
        }
    }
}
