using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    static class DividerTags
    {
        public static IDictionary<string, int> DivideTags(this IDictionary<string, int> tags, int fontMax = 150, int fontMin = 50)
        {
            if (fontMax <= 0 || fontMin <= 0)
                throw new ArgumentNullException("sizeAvgTagSize must be > 0");
            if (fontMin >= fontMax)
                throw new ArgumentNullException("fontMax must be larger than fontMin");
            var tagsDictionary= new Dictionary<string, int>();
            foreach (var tagKey in tags.Keys)
            {
                tagsDictionary[tagKey] = (int)Math.Round(tags[tagKey] == tags.Last().Value
                    ? (int)Math.Round((double)fontMin)
                    : tags[tagKey] / (double)tags.First().Value * (fontMax - fontMin) +
                      fontMin);
            }

            return tagsDictionary;
        }
    }
}