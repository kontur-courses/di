using System.ComponentModel;
using TagCloudContainer.Filters;
using TagCloudContainer.Formatters;
using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Parsers;
using TagCloudContainer.Readers;
using TagCloudContainer.TagsWithFont;
using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface.Settings
{
    public class PresetsSettings : IPresetsSettings
    {
        public PresetsSettings(ICloudDrawer drawer, IFileReader reader, IFileParser parser, IFilter filter,
            IWordFormatter formatter, IFrequencyCounter frequency, IFontSizer sizer)
        {
            Reader = reader;
            Parser = parser;
            Filter = filter;
            Formatter = formatter;
            FrequencyCounter = frequency;
            FontSizer = sizer;
            Drawer = drawer;

        }

        [DisplayName("Использовать .txt файл")] public Switcher txtReader { get; set; }

        [DisplayName("Фильтрация облака")] public Switcher Filtered { get; set; }

        [DisplayName("Только нижний регистр")] public Switcher ToLowerCase { get; set; }

        [DisplayName("Использовать палтиру")] public Switcher PaletteUse { get; set; }
        [Browsable(false)] public IFileReader Reader { get; }
        [Browsable(false)] public IFileParser Parser { get; }
        [Browsable(false)] public IFilter Filter { get; }
        [Browsable(false)] public IWordFormatter Formatter { get; }
        [Browsable(false)] public IFrequencyCounter FrequencyCounter { get; }
        [Browsable(false)] public IFontSizer FontSizer { get; }
        [Browsable(false)] public ICloudDrawer Drawer { get; }
    }
}
