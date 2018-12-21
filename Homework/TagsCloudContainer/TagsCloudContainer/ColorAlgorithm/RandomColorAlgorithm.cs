using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.ColorAlgorithm
{
    public class RandomColorAlgorithm : IColorAlgorithm
    {
        private readonly Random rnd = new Random();

        public Color GetColor(Dictionary<string, int> words = null, string word = "")
        {
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
    }
}
