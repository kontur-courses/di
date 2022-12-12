using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class FontSettingsAction : IUiAction
    {
        private readonly FontSettings settings;

        public FontSettingsAction(FontSettings settings)
        {
            this.settings = settings;
        }

        public Category Category => Category.Settings;
        public string Name => "Шрифт...";
        public string Description => "";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}