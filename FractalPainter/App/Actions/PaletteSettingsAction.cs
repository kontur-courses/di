using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class PaletteSettingsAction : IUiAction, INeed<Palette>
	{
		private Palette palette;
		public string Category => "Настройки";
		public string Name => "Палитра...";
		public string Description => "Цвета для рисования фракталов";

		public void Perform()
		{
			SettingsForm.For(palette).ShowDialog();
		}
		public void SetDependency(Palette dependency)
		{
			palette = dependency;
		}
	}
}