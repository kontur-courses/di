using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public class RandomColorGenerator : IColorGenerator
    {
        public IEnumerable<Color> GetColor()
        {
            var rnd = new Random();
            while (true)
                yield return Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
        }
    }
}