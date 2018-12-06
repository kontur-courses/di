using CloudLayouter.Infrastructer.Common;
using CloudLayouter.Infrastructer.Common.Settings;
using CloudLayouter.Infrastructer.Interfaces;

namespace CloudLayouter.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly Palette palette;

        public PaletteSettingsAction(Palette palette)
        {
            this.palette = palette;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Color...";
        public string Description => "Цветовая схема изображения";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}