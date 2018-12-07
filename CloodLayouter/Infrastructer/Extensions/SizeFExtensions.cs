using System;
using System.Drawing;

namespace CloodLayouter.Infrastructer.Extensions
{
    public static class SizeFExtensions
    {
        public static Size ToSizeI(this SizeF sizeF)
        {
            return new Size((int)Math.Ceiling(sizeF.Width),(int)Math.Ceiling(sizeF.Height));
        }
    }
}