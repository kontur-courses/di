using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloud.ColoringAlgorithms
{
    public interface IColoringAlgorithm
    {
        public Color GetNextColor();
    }
}
