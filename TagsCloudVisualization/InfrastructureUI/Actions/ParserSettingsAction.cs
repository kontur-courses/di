using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class ParserSettingsAction : IUiAction
    {
        private readonly ParserSettings settings;

        public ParserSettingsAction(ParserSettings settings)
        {
            this.settings = settings;
        }

        public Category Category => Category.Settings;
        public string Name => "Парсер...";
        public string Description => "";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}