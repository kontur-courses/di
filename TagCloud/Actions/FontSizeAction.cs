using TagCloud.Settings;

namespace TagCloud.Actions
{
    public class FontSizeAction : IUiAction
    {
        private readonly FontSettings fontSettings;
        public FontSizeAction(FontSettings fontSettings)
        {
            this.fontSettings = fontSettings;
        }
        public string Category => "Settings";
        public string Name => "Font Size";
        public string Description => "Change Font Sizes";
        public void Perform()
        {
            SettingsForm.For(fontSettings).ShowDialog();
        }
    }
}