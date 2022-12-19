using TagCloudGUI.Interfaces;
using TagCloudGUI.Settings;

namespace TagCloudGUI.Actions
{

    public class PaletteAction : IActionForm
    {
        private Palette palette;

        public PaletteAction(Palette palette)
        {
            this.palette = palette;
        }

        public string Category => "Настройки";
        public string Name => "Цвет вывода";
        public string Description => "Цвета для рисования облака";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}
