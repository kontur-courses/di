using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Algorithms;

namespace TagsCloudContainer
{
    public static class CircularCloudLayouterGenerator
    {
        public static Dictionary<string, (Rectangle, int)> GenerateRectanglesSet(CircularCloudAlgorithm layouter, 
            IEnumerable<KeyValuePair<string, int>> words,
            int widthBottomBound, int widthTopBound, int heightBottomBound, int heightTopBound)
        {
            //var rectangles = new List<Rectangle>();

            var result = new Dictionary<string, (Rectangle, int)>();

            var random = new Random();

            var wordsList = words.ToList();

            foreach (var word in wordsList)
            {
                var randomSize = new Size(random.Next(word.Value - widthBottomBound, word.Value - widthTopBound + 50),
                    random.Next(word.Value - heightBottomBound, word.Value - heightTopBound + 50));

//                var size = new Size(Math.Abs(word.Value - 10000 / wordsList.Count())+1, Math.Abs(word.Value - 1000 / wordsList.Count())+1);
                var size = new Size(word.Value + (int)(word.Value * 1.5) + 100, (word.Value + (int)(word.Value * 1.5))/2 + 50);
//                var newRectangle = layouter.PutNextRectangle(randomSize);
                var newRectangle = layouter.PutNextRectangle(size);
//                rectangles.Add(newRectangle);

                var w = $"{word.Key}({word.Value})";
                result[w] = (newRectangle, word.Value);
            }

            return result;
//            return rectangles;
        }
    }
}
