using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class FontSettingsAction: IMenuAction
	{
		private readonly FontSettings _settings;
		public string Category { get; } = "Настройки";
		public string Name { get; } = "Шрифт";
		public string Description { get; } = "Изменить шрифт";

		public FontSettingsAction(FontSettings settings) => _settings = settings;

		public void Perform()
		{
			SettingsForm.For(_settings).ShowDialog();
		}
	}
}