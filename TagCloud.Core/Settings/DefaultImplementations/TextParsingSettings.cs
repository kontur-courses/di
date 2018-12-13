using TagCloud.Core.Settings.Interfaces;

namespace TagCloud.Core.Settings.DefaultImplementations
{
    public class TextParsingSettings : ITextParsingSettings
    {
        public int? MaxUniqueWordsCount { get; set; }
    }
}