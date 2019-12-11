using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.IServices;
using TagCloud.Models;

namespace TagCloud
{
    public class WordsToTagsParser : IWordsToTagsParser
    {
        private readonly IFontSettingsFactory fontSettingsFactory;

        public WordsToTagsParser(IFontSettingsFactory fontSettingsFactory)
        {
            this.fontSettingsFactory = fontSettingsFactory;
        }

        public List<Tag> GetTagsRectangles(Dictionary<string, int> words, ImageSettings imageSettings)
        {
            var fontSettings = fontSettingsFactory.CreateFontSettings(imageSettings.FontName);
            return words.Select(s => new Tag(s.Key, s.Value, GetFont(fontSettings, s.Value))).ToList();
        }

        private Font GetFont(FontSettings fontSettings, int count)
        {
            var fontSize = fontSettings.defaultFontSize + count * 3;
            return new Font(fontSettings.fontFamily, fontSize, fontSettings.fontStyle);
        }
    }
}