using System.Windows.Forms;
using TagCloud;

namespace GUITagClouder
{
    public class ClouderSettingsAction : IGuiAction
    {
        public ClouderSettingsAction(CloudSettings settings, IImageHolder imageHolder)
        {
            this.settings = settings;
            this.imageHolder = imageHolder;
        }

        private readonly CloudSettings settings;
        private readonly IImageHolder imageHolder;
        public string Category => "Настройки";
        public string Name => "Изображение...";
        public string Description => "Размеры изображения";

        public void Perform()
        {
            new CloudSettingsForm(settings).ShowDialog();
            imageHolder.RecreateImage(settings);
        }
    }
}