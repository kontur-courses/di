using System;
using System.Drawing;

namespace CloodLayouter.Infrastructer
{
    public static class SizeFExtenstions
    {
        public static Size ToSize(this SizeF sizeF)
        {
            return new Size((int) Math.Ceiling(sizeF.Width) + 1, (int) Math.Ceiling(sizeF.Height) + 1);
        }
    }
}