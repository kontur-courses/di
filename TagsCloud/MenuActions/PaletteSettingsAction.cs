using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class PaletteSettingsAction : IMenuAction
	{
		private Palette _palette;

		public PaletteSettingsAction(Palette palette) => _palette = palette;

		public string Category => "Настройки";
		public string Name => "Палитра";
		public string Description => "Цвета для рисования фракталов";

		public void Perform()
		{
			SettingsForm.For(_palette).ShowDialog();
		}
	}
}