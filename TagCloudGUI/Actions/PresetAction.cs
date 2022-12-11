using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface.Actions
{
    public class PresetAction : IActionForm
    {
        private PresetsSettings settings;

        public PresetAction(PresetsSettings settings)
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

    public enum Switcher
    {
        On = 0,
        Off = 1
    }
}
