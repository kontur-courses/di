using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Layouters;

namespace TagCloud.Creators
{
    public interface ITagCreator
    {
        Tag Create(string value, int frequency);
        IEnumerable<Tag> Create(Dictionary<string, int> wordsWithFrequency);
    }
}
