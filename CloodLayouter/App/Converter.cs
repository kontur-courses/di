using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class Converter : IConverter, ITagProvider
    {
        private readonly IImageHolder imageHolder;
        private readonly IWordProvider wordProvider;

        public Converter(IWordProvider wordProvider, IImageHolder imageHolder)
        {
            this.wordProvider = wordProvider;
            this.imageHolder = imageHolder;
        }

        public List<Tag> Convert()
        {
            var dict = new Dictionary<string, int>();
            foreach (var word in wordProvider.GetWords())
            {
                if (!dict.ContainsKey(word))
                    dict[word] = 0;
                dict[word]++;
            }

            var i = 0;

            return dict.OrderBy(x => x.Value).Select(kvp =>
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
        }

        public List<Tag> GetTags()
        {
            return Convert();
        }
    }
}