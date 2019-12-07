using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TagsCloudContainer.OldTests
{
    static class Generator
    {
        public static List<Size> GetRandomSizesList(int minWidth, int maxWidth, int minHeight, int maxHeight, int numberOfSizes, Random random)
        {
            var sizes = new List<Size>();

            for (var i = 0; i < numberOfSizes; i++)
            {
                var width = random.Next(minWidth, maxWidth);
                var height = random.Next(minHeight, maxHeight);
                sizes.Add(new Size(width, height));
            }

            return sizes;
        }
    }
}
