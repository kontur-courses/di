using TagCloud;

namespace GUITagClouder
{
    public class DrawingSettingsAction : IGuiAction
    {
        public DrawingSettingsAction(DrawingSettings settings, CloudHolder imageHolder)
        {
            this.settings = settings;
            this.imageHolder = imageHolder;
        }

        private readonly DrawingSettings settings;
        private readonly CloudHolder imageHolder;
        public string Category => "Настройки";
        public string Name => "Отрисовка";
        public string Description => "Параметры рисования";

        public void Perform()
        {
            new DrawingSettingsForm(settings).ShowDialog();
            imageHolder.RecreateImage(settings);
        }
    }
}