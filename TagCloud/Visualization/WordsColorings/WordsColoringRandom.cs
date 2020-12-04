using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagCloud.Visualization.WordsColorings
{
    class WordsColoringRandom : IWordsColoring
    {
        private readonly Random random = new Random();
        public Color GetColor(string word, Rectangle location, TagCloud cloud) =>
            Color.FromArgb(255, random.Next(255), random.Next(255), random.Next(255));
    }
}
