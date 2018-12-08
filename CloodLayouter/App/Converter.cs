using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class Converter : IConverter
    {
        private readonly IImageHolder imageHolder;
        private readonly ITagProvider tagProvider;
        private readonly IWordProvider wordProvider;

        public Converter(IWordProvider wordProvider, ITagProvider tagProvider, IImageHolder imageHolder)
        {
            this.wordProvider = wordProvider;
            this.tagProvider = tagProvider;
            this.imageHolder = imageHolder;
        }

        public void Convert()
        {
            wordProvider.Words = wordProvider.Words.Select(x => x.ToLower()).ToList();
            var dict = new Dictionary<string, int>();
            foreach (var word in wordProvider.Words)
            {
                if (!dict.ContainsKey(word))
                    dict[word] = 0;
                dict[word]++;
            }

            var i = 0;

            var list = dict.OrderBy(x => x.Value).Select(kvp =>
            {
                var font = new Font(FontFamily.GenericSerif, 28 + 3 * i, FontStyle.Italic);
                i++;
                return new Tag
                {
                    Font = font,
                    Size = Graphics.FromImage(imageHolder.Image).MeasureString(kvp.Key, font).ToSizeI(),
                    Word = kvp.Key
                };
            }).ToList();
            tagProvider.Tags.AddRange(list);
        }
    }
}