using TagCloudContainer.Filters;
using TagCloudContainer.Formatters;
using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Parsers;
using TagCloudContainer.Readers;
using TagCloudContainer.TagsWithFont;
using TagCloudGraphicalUserInterface.Actions;

namespace TagCloudGraphicalUserInterface.Interfaces
{
    public interface IPresetsSettings
    {
        public Switcher txtReader { get; set; }
        public Switcher Filtered { get; set; }
        public Switcher ToLowerCase { get; set; }
        public IFileReader Reader { get; }
        public IFileParser Parser { get; }
        public IFilter Filter { get; }
        public IWordFormatter Formatter { get; }
        public IFrequencyCounter FrequencyCounter { get; }
        public IFontSizer FontSizer { get; }
        public ICloudDrawer Drawer { get; }

    }
}
