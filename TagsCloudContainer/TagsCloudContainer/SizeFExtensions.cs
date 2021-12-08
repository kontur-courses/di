using System;
using System.Drawing;

namespace TagsCloudContainer
{
    public static class SizeFExtensions
    {
        public static Size ToSizeCeiling(this SizeF sizeF) =>
            new((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
    }
}