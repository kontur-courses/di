using TagsCloudContainer.Settings;

namespace TagsCloudContainer.UI.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly Palette palette;

        public PaletteSettingsAction(Palette palette)
        {
            this.palette = palette;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Палитра...";
        public string Description => "Цвета для рисования облака";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}