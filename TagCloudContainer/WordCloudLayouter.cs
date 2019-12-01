using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudContainer
{
    public class WordCloudLayouter
    {
        public CircularCloudLayouter RectangleLayouter;
        
        public WordCloudLayouter()
        {
            RectangleLayouter = new CircularCloudLayouter();
        }


        public void AddWords(IEnumerable<(string word, int occurrenceCount)> words)
        {
            //todo sort tuples by occurenceCount and start adding words to layouter from biggest to smallest
        }
    }
}