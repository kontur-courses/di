using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class DefaultPaintingAlgorithm : IPaintingAlgorithm
    {
        private Random rnd;

        public DefaultPaintingAlgorithm()
        {
            rnd = new Random();
        }

        public List<Color> GetColorForTag(IEnumerable<Tag> tags)
        {
            var result = new List<Color>();
            foreach (var tag in tags)
                result.Add(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
            return result;
        }
    }
}