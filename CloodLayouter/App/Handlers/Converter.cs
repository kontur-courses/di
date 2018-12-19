using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class Converter : IConverter, IProvider<IEnumerable<Tag>>
    {
        private readonly IProvider<Bitmap> imageHolder;
        private readonly IProvider<IEnumerable<string>> wordProvider;

        public Converter(IProvider<IEnumerable<string>> wordProvider, IProvider<Bitmap> imageHolder)
        {
            this.wordProvider = wordProvider;
            this.imageHolder = imageHolder;
        }

        public List<Tag> Convert()
        {
            var dict = new Dictionary<string, int>();
            foreach (var word in wordProvider.Get())
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
                    Size = Graphics.FromImage(imageHolder.Get()).MeasureString(kvp.Key, font).ToSizeI(),
                    Word = kvp.Key
                };
            }).ToList();
        }

        public IEnumerable<Tag> Get()
        {
            return Convert();
        }
    }
}