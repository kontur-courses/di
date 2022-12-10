using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly Palette palette;

        public PaletteSettingsAction(Palette palette)
        {
            this.palette = palette;
        }

        public string Category => "Настройки";
        public string Name => "Палитра...";
        public string Description => "Цвета для рисования";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}