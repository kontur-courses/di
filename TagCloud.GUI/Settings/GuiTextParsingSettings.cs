using TagCloud.Core.Settings.DefaultImplementations;

namespace TagCloud.GUI.Settings
{
    public class GuiTextParsingSettings : TextParsingSettings, ISettings
    {
        public string GetSettingsName()
        {
            return "Text parsing settings";
        }
    }
}