using System.Windows.Forms;
using TagCloud;

namespace GUITagClouder
{
    public class ClouderSettingsAction : IGuiAction
    {
        public ClouderSettingsAction(CloudSettings settings, CloudProcessor imageProcessor)
        {
            this.settings = settings;
            this.imageProcessor = imageProcessor;
        }

        private readonly CloudSettings settings;
        private readonly CloudProcessor imageProcessor;
        public string Category => "Настройки";
        public string Name => "Облако";
        public string Description => "Типы обработчиков";

        public void Perform()
        {
            new CloudSettingsForm(settings).ShowDialog();
            imageProcessor.RecreateImage(settings);
        }
    }
}