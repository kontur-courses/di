using System.ComponentModel;
using System.Runtime.Serialization;
using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Interfaces;
using TagCloudContainer.Readers;
using TagCloudContainer.TagsWithFont;
using TagCloudGUI.Interfaces;

namespace TagCloudGUI.Settings
{
    public class AlgorithmSettings : IAlgorithmSettings, IPresetsSettings
    {
        public AlgorithmSettings(IFontSettings fontSettings, ICloudDrawer drawer, IFileReader reader, IFileParser parser,
            IWordProcessor formatter, IFrequencyCounter frequency, IFontSizer sizer)
        {
            FontSettings = fontSettings;

            Reader = reader;
            Parser = parser;
            Formatter = formatter;
            FrequencyCounter = frequency;
            FontSizer = sizer;
            Drawer = drawer;
        }

        [Browsable(false)] public IFontSettings FontSettings { get; set; }

        [DisplayName("Шрифт")]
        public string FontFamilyName
        {
            get => FontSettings.Font.Name;
            set => FontSettings.Font = new FontFamily(value);
        }

        [DisplayName("Максимальный шрифт")]
        public int MaxFont
        {
            get => FontSettings.MaxFontSize;
            set => FontSettings.MaxFontSize = value;
        }

        [DisplayName("Минимальный шрифт")]
        public int MinFont
        {
            get => FontSettings.MinFontSize;
            set => FontSettings.MinFontSize = value;
        }

        [DisplayName("Путь к файлу")] public string ImagesDirectory { get; set; }

        [DisplayName("Удалять мусор")] public Switcher Filtered { get; set; }
        [DisplayName("Нижний регистр")] public Switcher ToLowerCase { get; set; }

        [DisplayName("Упорядочить слова")] public Switcher UseSort { get; set; }

        [Browsable(false)] public IFileReader Reader { get; }
        [Browsable(false)] public IFileParser Parser { get; }
        [Browsable(false)] public IWordProcessor Formatter { get; }
        [Browsable(false)] public IFrequencyCounter FrequencyCounter { get; }
        [Browsable(false)] public IFontSizer FontSizer { get; }
        [Browsable(false)] public ICloudDrawer Drawer { get; }
    }
}
