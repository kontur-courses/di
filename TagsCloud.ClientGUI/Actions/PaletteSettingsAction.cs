using TagsCloud.ClientGUI.Infrastructure;

namespace TagsCloud.ClientGUI.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly Palette palette;
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;

        public PaletteSettingsAction(IImageHolder imageHolder, ImageSettings imageSettings, Palette paletteDependence)
        {
            palette = paletteDependence;
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
        }

        public string Category => "Настройки";
        public string Name => "Палитра...";
        public string Description => "Цвета для рисования фракталов";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
            imageHolder.RecreateImage(imageSettings);
        }
    }
}