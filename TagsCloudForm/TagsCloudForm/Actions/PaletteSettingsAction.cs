using TagsCloudForm.Common;
using TagsCloudForm.UiActions;

namespace TagsCloudForm.Actions
{
    internal class PaletteSettingsAction : IUiAction
    {
        private readonly IPalette palette;

        public PaletteSettingsAction(IPalette palette)
        {
            this.palette = palette;
        }

        public string Category => "Настройки";
        public string Name => "Палитра...";
        public string Description => "Цвета для рисования облака";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}