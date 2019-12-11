using TagCloud.CloudLayouter.CircularLayouter;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class PaintCloudAction : IUiAction
    {
        private readonly SpiralSettings spiralSettings;
        private readonly CloudPainter painter;

        public PaintCloudAction(CloudPainter painter, SpiralSettings spiralSettings)
        {
            this.spiralSettings = spiralSettings;
            this.painter = painter;
        }

        public string Category => "Нарисовать";
        public string Name => "Облако";
        public string Description => "Нарисовать облако тегов";

        public void Perform()
        {
            SettingsForm<SpiralSettings>.For(spiralSettings).ShowDialog();
            painter.Paint();
        }
    }
}