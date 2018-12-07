using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class Converter : IConverter
    {
        private readonly ITagDrawingSettingsProvider tagDrawingSettingsProvider;
        
        public Converter(ITagDrawingSettingsProvider tagDrawingSettingsProvider)
        {
            this.tagDrawingSettingsProvider = tagDrawingSettingsProvider;
        }
        
        public IEnumerable<Tag> Convert(IEnumerable<string> strings)
        {
            var dict = new Dictionary<string,int>();
            
            foreach (var word in strings)
            {
                if (!dict.ContainsKey(word))
                    dict[word] = 0;
                dict[word]++;               
            }

            var sortedWordList = dict.OrderBy(x => -x.Value).Select(x => x.Key).ToList();
            var shift = (tagDrawingSettingsProvider.MaxFontSize - tagDrawingSettingsProvider.MinFontSize) /
                        sortedWordList.Count;
            for (int i = 0; i < sortedWordList.Count; i++)
            {
                yield return new Tag(sortedWordList[i],
                    new Font(tagDrawingSettingsProvider.FontFamily, tagDrawingSettingsProvider.MaxFontSize - shift * i,
                        tagDrawingSettingsProvider.FontStyle));
            }
        }
    }
}