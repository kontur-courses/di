using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer
{
    public class FixedColorProvider : IColorProvider
    {
        private Color color;

        public FixedColorProvider(Color color)
        {
            this.color = color;
        }
        public Color GetNextColor()
        {
            return color;
        }
    }
}
