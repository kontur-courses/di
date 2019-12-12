using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class PaletteSettingsAction : IMenuAction
	{
		private Palette palette;

		public PaletteSettingsAction(Palette palette) => this.palette = palette;

		public string Category => "Настройки";
		public string Name => "Палитра";
		public string Description => "Цвета для рисования фракталов";

		public void Perform()
		{
			SettingsForm.For(palette).ShowDialog();
		}
	}
}