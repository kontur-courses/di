using TagCloud.Core.Settings.DefaultImplementations;

namespace TagCloud.GUI.Settings
{
    public class GuiLayoutingSettings : LayoutingSettings, ISettings
    {
        public string GetSettingsName()
        {
            return "Layouting settings";
        }
    }
}