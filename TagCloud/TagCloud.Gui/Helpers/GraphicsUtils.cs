using System;
using System.Drawing;

namespace TagCloud.Gui.Helpers
{
    internal static class GraphicsUtils
    {
        public static Image PlaceAtCenter(Image source, Size newSize)
        {
            var resizeCoefficient = Math.Min(
                (float) newSize.Height / source.Height,
                (float) newSize.Width / source.Width);
            var resized = new Bitmap(source, (source.Size * resizeCoefficient).ToSize());
            var location = new Point((newSize - resized.Size) / 2);

            var result = new Bitmap(newSize.Width, newSize.Height);
            using (var g = Graphics.FromImage(result))
                g.DrawImage(resized, location);
            return result;
        }
    }
}