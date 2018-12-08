using System;
using System.Drawing;

namespace CloodLayouter.Infrastructer
{
    public static class SizeFExtenstions
    {
        public static Size ToSizeI(this SizeF sizeF)
        {
            return new Size(Convert.ToInt32(Math.Ceiling(sizeF.Width)),
                Convert.ToInt32((int) Math.Ceiling(sizeF.Height)));
        }
    }
}