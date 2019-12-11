using TagCloud.Visualization;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class ViewSettingsAction : IUiAction
    {
        private readonly ViewSettings viewSettings;
        private readonly CloudPainter cloudPainter;

        public ViewSettingsAction(ViewSettings viewSettings, CloudPainter cloudPainter)
        {
            this.viewSettings = viewSettings;
            this.cloudPainter = cloudPainter;
        }

        public string Category => "Вид";
        public string Name => "Изменить";
        public string Description => "Изменить внешний вид";

        public void Perform()
        {
            SettingsForm<ViewSettings>.For(viewSettings).ShowDialog();
            cloudPainter.Paint();
        }
    }
}