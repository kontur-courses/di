using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class RandomColorPicker : IColorPicker
    {
        private Random random;

        public RandomColorPicker()
        {
            random = new Random();
        }

        public Brush GenerateColor()
        {
            return new SolidBrush(Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
        }
    }
}
