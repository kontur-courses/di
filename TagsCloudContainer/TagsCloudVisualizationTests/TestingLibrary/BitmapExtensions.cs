using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualizationTests.TestingLibrary
{
    public static class BitmapExtensions
    {
        public static IEnumerable<Color> ToEnumerable(this Bitmap bitmap)
        {
            for (var i = 0; i < bitmap.Width; i++)
                for (var j = 0; j < bitmap.Height; j++)
                    yield return bitmap.GetPixel(i, j);
        }
    }
}