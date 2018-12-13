using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualisation.Coloring
{
    public class RandomColorManager : IColorManager
    {
        public Dictionary<TagsCloudWord, Color> GetWordsColors(List<TagsCloudWord> words)
        {
            var rnd = new Random();
            var result = new Dictionary<TagsCloudWord, Color>();
            foreach (var word in words)
            {
                var color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                result[word] = color;
            }

            return result;
        }
    }
}