using TagCloud.Core.Settings.DefaultImplementations;

namespace TagCloud.GUI.Settings
{
    public class GuiPaintingSettings : PaintingSettings, ISettings
    {
        public string GetSettingsName()
        {
            return "Painting settings";
        }
    }
}