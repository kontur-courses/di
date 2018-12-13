using TagCloud.Core.Settings.DefaultImplementations;

namespace TagCloud.GUI.Settings
{
    public class GuiTagCloudSettings : TagCloudSettings, ISettings
    {
        public string GetSettingsName()
        {
            return "Tag cloud settings";
        }
    }
}