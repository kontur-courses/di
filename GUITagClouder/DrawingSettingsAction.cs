using TagCloud;

namespace GUITagClouder
{
    public class DrawingSettingsAction : IGuiAction
    {
        public DrawingSettingsAction(DrawingSettings settings, IImageHolder imageHolder)
        {
            this.settings = settings;
            this.imageHolder = imageHolder;
        }

        private readonly DrawingSettings settings;
        private readonly IImageHolder imageHolder;
        public string Category => "Настройки";
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            new DrawingSettingsForm(settings).ShowDialog();
            imageHolder.RecreateImage(settings);
        }
    }
}