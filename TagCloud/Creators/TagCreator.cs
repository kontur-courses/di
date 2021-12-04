using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Layouters;

namespace TagCloud.Creators
{
    public class TagCreator : ITagCreator
    {
        public Tag Create(string value, int frequency)
        {
            var size = new Size(value.Length * 5, frequency * 3);
            return new Tag(value, frequency, size);
        }

        public IEnumerable<Tag> Create(Dictionary<string, int> wordsWithFrequency)
        {
            return wordsWithFrequency.Select(pair => Create(pair.Key, pair.Value));
        }
    }
}
