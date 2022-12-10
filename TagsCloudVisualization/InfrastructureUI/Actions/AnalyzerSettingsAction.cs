using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class AnalyzerSettingsAction : IUiAction
    {
        private readonly AnalyzerSettings settings;

        public AnalyzerSettingsAction(AnalyzerSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Настройки";
        public string Name => "анализатор...";
        public string Description => "";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}