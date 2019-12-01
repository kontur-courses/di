using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class ImageSettings
    {
        public int Heigth { get; }
        public int Width { get; }

        public ImageSettings(int heigth, int width)
        {
            Heigth = heigth;
            Width = width;
        }
    }
}
