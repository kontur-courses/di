using TagCloud.Visualization;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class ViewSettingsAction : IUiAction
    {
        private readonly ViewSettings viewSettings;

        public ViewSettingsAction(ViewSettings viewSettings)
        {
            this.viewSettings = viewSettings;
        }

        public string Category => "Вид";
        public string Name => "Изменить";
        public string Description => "Изменить внешний вид";

        public void Perform()
        {
            SettingsForm<ViewSettings>.For(viewSettings).ShowDialog();
        }
    }
}