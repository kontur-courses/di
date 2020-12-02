using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloud.ColoringAlgorithms
{
    public class BlackColoringAlgorithm : IColoringAlgorithm
    {
        public Color GetNextColor()
        {
            return Color.Black;
        }
    }
}
