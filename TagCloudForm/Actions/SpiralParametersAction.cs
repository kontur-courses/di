using TagCloud.CloudLayouter;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class SpiralParametersAction : IUiAction
    {
        private readonly SpiralSettings spiralSettings;
        private readonly CloudPainter painter;

        public SpiralParametersAction(CloudPainter painter, SpiralSettings spiralSettings)
        {
            this.spiralSettings = spiralSettings;
            this.painter = painter;
        }

        public string Category { get; } = "Спираль";
        public string Name { get; } = "Настроить";
        public string Description { get; } = "Настроить параметры спирали";

        public void Perform()
        {
            SettingsForm.For(spiralSettings).ShowDialog();
            painter.Paint();
        }
    }
}