using System.ComponentModel;
using TagCloudContainer.Interfaces;
using TagCloudGUI.Interfaces;

namespace TagCloudGUI.Settings
{
    public class PresetsSettings : IPresetsSettings
    {
        public PresetsSettings(ICloudDrawer drawer, IFileReader reader, IFileParser parser,
            IWordFormatter formatter, IFrequencyCounter frequency, IFontSizer sizer)
        {
            Reader = reader;
            Parser = parser;
            Formatter = formatter;
            FrequencyCounter = frequency;
            FontSizer = sizer;
            Drawer = drawer;

        }

        [DisplayName("Загружать txt вместо doc")] public Switcher TxtReader { get; set; }
        [DisplayName("Удалять мусор")] public Switcher Filtered { get; set; }
        [DisplayName("Нижний регистр")] public Switcher ToLowerCase { get; set; }

        [Browsable(false)] public IFileReader Reader { get; }
        [Browsable(false)] public IFileParser Parser { get; }
        [Browsable(false)] public IWordFormatter Formatter { get; }
        [Browsable(false)] public IFrequencyCounter FrequencyCounter { get; }
        [Browsable(false)] public IFontSizer FontSizer { get; }
        [Browsable(false)] public ICloudDrawer Drawer { get; }
    }
}
