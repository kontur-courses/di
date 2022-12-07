using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TagCloud.PointGenerators;
using TagCloud.Tag;
using TagCloud.WordPreprocessors;

namespace TagCloud
{
    public class WordsTagCloud : TagCloud
    {
        protected WordsTagCloud(Point center) : base(center)
        {
        }

        public static WordsTagCloud Create(IWordPreprocessor wordPreprocessor, IPointGenerator pointGenerator)
        {
            var wordsTagCloud = new WordsTagCloud(pointGenerator.GetCenterPoint());
            var circularCloudLayouter = new CircularCloudLayouter(pointGenerator);
            if (wordPreprocessor == null)// || settings == null)
                throw new ArgumentNullException(
                    "IWordPreprocessor and TagCloudSettings are required for this method");

            var words = wordPreprocessor.GetPreprocessedWords();
            var groupedWords = words.GroupBy(word => word).
                OrderByDescending(group => group.Count());

            foreach (var word in groupedWords)
            {
                var font = new Font("Arial", word.Count());
                wordsTagCloud.Rectangles.Add(new Word(word.Key, font));
                wordsTagCloud.Rectangles.Last().Frame = circularCloudLayouter.
                    PutNextRectangle(wordsTagCloud.Rectangles.Last().Size);
            }
            return wordsTagCloud;
        }
    }
}
