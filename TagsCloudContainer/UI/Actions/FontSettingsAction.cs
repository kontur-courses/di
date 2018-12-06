using TagsCloudContainer.Settings;

namespace TagsCloudContainer.UI.Actions
{
    public class FontSettingsAction : IUiAction
    {
        private readonly FontSettings fontSettings;

        public FontSettingsAction(FontSettings fontSettings)
        {
            this.fontSettings = fontSettings;
        }

        public MenuCategory Category => MenuCategory.Settings;
        public string Name => "Шрифты...";
        public string Description => "Шрифты для рисования облака";

        public void Perform()
        {
            SettingsForm.For(fontSettings).ShowDialog();
        }
    }
}
