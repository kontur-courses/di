using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public class RandomColorGenerator : IColorGenerator
    {
        public Stack<Color> GetColors(int count)
        {
            var colors = new Stack<Color>();
            var rnd = new Random();
            for(var i = 0; i < count; i++)
                colors.Push(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)));
            return colors;
        }
    }
}