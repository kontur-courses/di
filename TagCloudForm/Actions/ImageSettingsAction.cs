using TagCloud.Visualization;
using TagCloudForm.Settings;

namespace TagCloudForm.Actions
{
    public class ImageSettingsAction : IUiAction
    {
        private readonly ImageSettings imageSettings;
        private readonly CloudPainter cloudPainter;

        public ImageSettingsAction(ImageSettings imageSettings, CloudPainter cloudPainter)
        {
            this.imageSettings = imageSettings;
            this.cloudPainter = cloudPainter;
        }

        public string Category { get; } = "Изображение";
        public string Name { get; } = "Настроить";
        public string Description { get; } = "Настроить размеры изображения";

        public void Perform()
        {
            SettingsForm<ImageSettings>.For(imageSettings).ShowDialog();
            cloudPainter.Paint();
        }
    }
}