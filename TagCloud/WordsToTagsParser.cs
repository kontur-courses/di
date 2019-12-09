using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Models;

namespace TagCloud
{
    public class WordsToTagsParser : IWordsToTagsParser
    {
        public List<Tag> GetTagsRectangles(Dictionary<string, int> words)
        {
            var fontSettings = new FontSettings();
            return words.Select(s => new Tag(s.Key, s.Value, GetFont(fontSettings, s.Value))).ToList();
        }

        private Font GetFont(FontSettings fontSettings, int count)
        {
            var fontSize = fontSettings.defaultFontSize + count * 3;
            return new Font(fontSettings.fontFamily, fontSize, fontSettings.fontStyle);
        }
    }
}