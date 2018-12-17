using System.Windows.Forms;
using TagCloud;

namespace GUITagClouder
{
    public class ClouderSettingsAction : IGuiAction
    {
        public ClouderSettingsAction(CloudSettings settings, CloudHolder imageHolder)
        {
            this.settings = settings;
            this.imageHolder = imageHolder;
        }

        private readonly CloudSettings settings;
        private readonly CloudHolder imageHolder;
        public string Category => "Настройки";
        public string Name => "Облако";
        public string Description => "Типы обработчиков";

        public void Perform()
        {
            new CloudSettingsForm(settings).ShowDialog();
            imageHolder.RecreateImage(settings);
        }
    }
}