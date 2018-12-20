using TagCloud;

namespace GUITagClouder
{
    public class DrawingSettingsAction : IGuiAction
    {
        public DrawingSettingsAction(DrawingSettings settings, CloudProcessor imageProcessor)
        {
            this.settings = settings;
            this.imageProcessor = imageProcessor;
        }

        private readonly DrawingSettings settings;
        private readonly CloudProcessor imageProcessor;
        public string Category => "Настройки";
        public string Name => "Отрисовка";
        public string Description => "Параметры рисования";

        public void Perform()
        {
            new DrawingSettingsForm(settings).ShowDialog();
            imageProcessor.RecreateImage(settings);
        }
    }
}