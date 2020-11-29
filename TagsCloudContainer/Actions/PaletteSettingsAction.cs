using TagsCloudContainer.Common;
using TagsCloudContainer.UiActions;

namespace TagsCloudContainer.Actions
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
        public string Description => "Цвета для рисования облака тегов";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}