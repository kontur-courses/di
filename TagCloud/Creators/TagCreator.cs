using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagCloud.Creators
{
    public class TagCreator : ITagCreator
    {
        private readonly Font font;

        public TagCreator(Font font)
        {
            this.font = font;
        }

        public Tag Create(string value, int frequency)
        {
            Size size;
            using (var renderFont = new Font(font.FontFamily, font.Size * frequency))
                size = TextRenderer.MeasureText(value, renderFont);
            
            return new Tag(value, frequency, size);
        }

        public IEnumerable<Tag> Create(Dictionary<string, int> wordsWithFrequency)
        {
            return wordsWithFrequency.Select(pair => Create(pair.Key, pair.Value));
        }
    }
}
