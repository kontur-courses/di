using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer
{
    public class FixedColorProvider : IColorProvider
    {
        private readonly Color color;
        public FixedColorProvider()
        {
            color = Color.Black;
        }
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
