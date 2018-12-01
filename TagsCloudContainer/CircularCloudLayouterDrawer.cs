using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    static class CircularCloudLayouterDrawer
    {
        public static void DrawRectanglesSet(Size size, string outputFileName,
            List<Rectangle> rectangles)
        {
            using (var bitmap = new Bitmap(size.Width, size.Height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    foreach (var rectangle in rectangles)
                    {
                        graphics.DrawRectangle(new Pen(Color.Red), rectangle);
                    }
                    bitmap.Save(outputFileName);
                }
            }
        }
    }
}
