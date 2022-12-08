using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Helpers
{
    public class TagCloudHelper
    {
        public static List<Tag> CreateTagsFromWords(IEnumerable<string> words, int minFontSize = 10, int maxFontSize = 50)
        {
            var tags = new List<Tag>();
            var dict = new Dictionary<string, int>();
            
            foreach (var word in words)
            {
                if (!dict.ContainsKey(word))
                    dict[word] = 0;
                dict[word]++;
            }

            if (dict.Count == 0)
                return tags;

            var minFrequency = dict.Min(x => x.Value);
            var maxFrequency = dict.Max(x => x.Value);

            foreach (var (text, frequency) in dict)
            {
                var fontSize = maxFontSize * (frequency - minFrequency) / (maxFrequency - minFrequency);

                if (fontSize < minFontSize)
                    fontSize = minFontSize;
                
                tags.Add(new Tag(text, fontSize, frequency));
            }

            return tags;
        }

        public static void ShuffleTags(List<Tag> tags, List<Size> sizes)
        {
            var random = new Random();

            for (var i = tags.Count - 1; i >= 1 ; i--)
            {
                var j = random.Next(i + 1);
                (tags[j], tags[i]) = (tags[i], tags[j]);
                (sizes[j], sizes[i]) = (sizes[i], sizes[j]);
            }
        }

        public static List<Size> GenerateRectangleSizes(List<Tag> tags)
        {
            using var gp = Graphics.FromImage(new Bitmap(1, 1));

            return tags.Select(tag => gp.MeasureString(tag.Text, new Font("Arial", tag.FontSize)).ToSize()).ToList();
        }
    }
}