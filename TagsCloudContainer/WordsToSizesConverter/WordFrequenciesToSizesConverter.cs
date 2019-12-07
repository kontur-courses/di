using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TagsCloudContainer.WordProcessor;

namespace TagsCloudContainer.WordsToSizesConverter
{
    public class WordFrequenciesToSizesConverter : IWordFrequenciesToSizesConverter
    {
        public IEnumerable<Size> ConvertToSizes(IEnumerable<WordWithCount> wordsWithCount)
        {
            var defaultSize = new Size(20, 20);
            var sizes = new List<Size>();
            foreach (var wordWithCount in wordsWithCount)
            {
                var size = new Size((int)(defaultSize.Width * wordWithCount.Word.Length * 0.8), defaultSize.Height);
                size = new Size(size.Width * wordWithCount.Count, size.Height * wordWithCount.Count);
                sizes.Add(size);
            }

            return sizes;
        }
    }
}
