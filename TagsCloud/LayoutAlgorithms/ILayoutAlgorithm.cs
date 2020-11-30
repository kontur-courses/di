using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloud.LayoutAlgorithms
{
    interface ILayoutAlgorithm
    {
        public Rectangle PutNextRectangle(Size size);
        public Size GetSize();
    }
}
