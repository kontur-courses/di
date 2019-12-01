using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step08.Infrastructure.Injection;
using FractalPainting.Solved.Step08.Infrastructure.UiActions;

namespace FractalPainting.Solved.Step08.App.Actions
{
    public class PaletteSettingsAction : IUiAction, INeed<Palette>
    {
        private Palette palette;

        public void SetDependency(Palette dependency)
        {
            palette = dependency;
        }

        public string Category => "Настройки";
        public string Name => "Палитра...";
        public string Description => "Цвета для рисования фракталов";

        public void Perform()
        {
            SettingsForm.For(palette).ShowDialog();
        }
    }
}