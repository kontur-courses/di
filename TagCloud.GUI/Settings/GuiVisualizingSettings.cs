using TagCloud.Core.Settings.DefaultImplementations;

namespace TagCloud.GUI.Settings
{
    public class GuiVisualizingSettings : VisualizingSettings, ISettings
    {
        public string GetSettingsName()
        {
            return "Visualizing settings";
        }
    }
}