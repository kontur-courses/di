using TagsCloudForm.Common;
using TagsCloudForm.UiActions;

namespace TagsCloudForm.Actions
{
    internal class CircularCloudLayouterSettingsAction : IUiAction
    {
        private readonly CircularCloudLayouterSettings.CircularCloudLayouterSettings settings;

        public CircularCloudLayouterSettingsAction(CircularCloudLayouterSettings.CircularCloudLayouterSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Настройки";
        public string Name => "Облако...";
        public string Description => "Настройки облака";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}
