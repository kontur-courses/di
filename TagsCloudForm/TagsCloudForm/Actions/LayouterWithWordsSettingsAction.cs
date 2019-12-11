using TagsCloudForm.Common;
using TagsCloudForm.UiActions;

namespace TagsCloudForm.Actions
{
    public class LayouterWithWordsSettingsAction : IUiAction
    {
        private readonly CircularCloudLayouterSettings.CircularCloudLayouterWithWordsSettings settings;

        public LayouterWithWordsSettingsAction(CircularCloudLayouterSettings.CircularCloudLayouterWithWordsSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Настройки";
        public string Name => "Облако с словами...";
        public string Description => "Настройки облака";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }
}
