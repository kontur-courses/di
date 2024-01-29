using TagsCloudContainer.Infrastucture.Settings;
using TagsCloudContainer.Infrastucture.UiActions;

namespace TagsCloudContainer.Actions
{
    public class AlgorithmSettingsAction : IUiAction
    {
        private AlgorithmSettings algorithmSettings;

        public AlgorithmSettingsAction(AlgorithmSettings settings)
        {
            this.algorithmSettings = settings;
        }

        public string Category => "Настроить";

        public string Name => "Алгоритм";

        public string Description => "Изменить настройки алгоритма";

        public void Perform()
        {
            SettingsForm.For(algorithmSettings).ShowDialog();
        }
    }
}