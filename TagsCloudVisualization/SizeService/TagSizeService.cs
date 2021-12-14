using System;
using System.Drawing;

namespace TagsCloudVisualization.SizeService
{
    internal class TagSizeService : ITagSizeService
    {
        public Size GetSize(Tag tag, Font font)
        {
            using var graphics = Graphics.FromHwnd(IntPtr.Zero);
            var size = graphics.MeasureString(tag.Word, font);
            return Size.Ceiling(size);
        }
    }
}