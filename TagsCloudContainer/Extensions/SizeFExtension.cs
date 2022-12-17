using System;
using System.Drawing;

namespace TagsCloudContainer.Extensions
{
    public static class SizeFExtension
    {
        public static Size Ceiling(this SizeF sizeF)
        {
            var width = (int)Math.Ceiling(sizeF.Width);
            var height = (int)Math.Ceiling(sizeF.Height);
            return new Size(width, height);
        }
    }
}