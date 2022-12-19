using TagCloudContainer.Interfaces;
using TagCloudGUI.Settings;

namespace TagCloudGUI.Interfaces
{
    public interface IPresetsSettings
    {
        public Switcher TxtReader { get; set; }
        public Switcher Filtered { get; set; }
        public Switcher ToLowerCase { get; set; }
        public IFileReader Reader { get; }
        public IFileParser Parser { get; }
        public IWordFormatter Formatter { get; }
        public IFrequencyCounter FrequencyCounter { get; }
        public IFontSizer FontSizer { get; }
        public ICloudDrawer Drawer { get; }

    }
}
