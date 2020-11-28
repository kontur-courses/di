using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class SizesGenerator : ISizesGenerator
    {
        public Dictionary<string, Size> GenerateSizes(Dictionary<string, int> dictionary)
        {
            var charWidth = 5;
            var sizes = new Dictionary<string, Size>();
            var coefficient = 1.1;
            foreach (var pair in dictionary)
            {
                var word = pair.Key;
                var wordCount = pair.Value;
                sizes[word] = new Size((int) (charWidth * word.Length * Math.Pow(coefficient, wordCount)),
                    (int) (charWidth * Math.Pow(coefficient, wordCount)));
            }

            return sizes;
        }
    }
}