using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class PainterSettingsAction: IMenuAction
	{
		private readonly PainterSettings painterSettings;

		public PainterSettingsAction(PainterSettings painterSettings) => this.painterSettings = painterSettings;

		public string Category { get; } = "Настройки";
		public string Name { get; } = "Отрисовка";
		public string Description { get; } = "Настройки отрисовки";
		
		public void Perform()
		{
			SettingsForm.For(painterSettings).ShowDialog();
		}
	}
}