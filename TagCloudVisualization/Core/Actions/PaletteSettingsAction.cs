using TagCloudVisualization.Infrastructure.Common;
using TagCloudVisualization.Infrastructure.UiActions;

namespace TagCloudVisualization.Core.Actions
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
        public string Description => "Цвета для рисования фракталов";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}