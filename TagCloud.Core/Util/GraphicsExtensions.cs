using System.Drawing;
using TagCloud.Util;

namespace TagCloud.Core.Util
{
    public static class GraphicsExtensions
    {
        public static void DrawTag(this Graphics graphics, Tag tag)
        {
            graphics.DrawString(tag.Word, tag.Font, tag.Brush, tag.Place);
        }
    }
}