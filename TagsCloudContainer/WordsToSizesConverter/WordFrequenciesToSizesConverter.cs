using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.WordsToSizesConverter
{
    public class WordFrequenciesToSizesConverter : IWordFrequenciesToSizesConverter
    {
        public IList<Size> ConvertToSizes(IDictionary<string, int> wordFrequencies)
        {
            var defaultSize = new Size(20, 20);
            var sizes = new List<Size>();
            foreach (var word in wordFrequencies.Keys)
            {
                var size = new Size((int)(defaultSize.Width * word.Length * 0.8), defaultSize.Height);
                size = new Size(size.Width * wordFrequencies[word], size.Height * wordFrequencies[word]);
                sizes.Add(size);
            }

            return sizes;
        }
    }
}
