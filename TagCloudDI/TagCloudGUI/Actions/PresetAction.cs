using TagCloudGUI.Interfaces;
using TagCloudGUI.Settings;

namespace TagCloudGUI.Actions
{
    public class PresetAction : IActionForm
    {
        private IPresetsSettings settings;

        public PresetAction(IPresetsSettings settings)
        {
            this.settings = settings;
        }

        public string Category => "Настройки";
        public string Name => "Преднастройки";
        public string Description => "Преднастройки облака";

        public void Perform()
        {
            SettingsForm.For(settings).ShowDialog();
        }
    }


}
