using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class PaletteSettingsAction : IUiAction
    {
        private readonly PaletteSettings settings;

        public PaletteSettingsAction(PaletteSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Настройки";
        public string Name => "палитры...";
        public string Description => "";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}