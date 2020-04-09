using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly Palette palette;

        public PaletteSettingsAction(Palette palette)
        {
            this.palette = palette;
        }

        #region IUiAction

        public string Category => "Настройки";
        public int Order => 1;
        public int CategoryOrder => 2;
        public string Name => "Палитра...";
        public string Description => "Цвета для рисования фракталов";

        public void Perform() => SettingsForm.For(palette).ShowDialog();

        #endregion
    }
}