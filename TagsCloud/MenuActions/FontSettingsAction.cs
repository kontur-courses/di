using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class FontSettingsAction: IMenuAction
	{
		private readonly FontSettings settings;
		public string Category { get; } = "Настройки";
		public string Name { get; } = "Шрифт";
		public string Description { get; } = "Изменить шрифт";

		public FontSettingsAction(FontSettings settings) => this.settings = settings;

		public void Perform()
		{
			SettingsForm.For(settings).ShowDialog();
		}
	}
}