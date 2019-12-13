using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class DefaultTagsPaintingAlgorithm : ITagsPaintingAlgorithm
    {
        private Random rnd;

        public DefaultTagsPaintingAlgorithm()
        {
            rnd = new Random();
        }

        public List<Color> GetColorForTag(IEnumerable<Tag> tags)
        {
            return tags
                .Select(tag => Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)))
                .ToList();
        }
    }
}