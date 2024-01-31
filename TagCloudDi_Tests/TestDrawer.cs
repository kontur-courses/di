using System.Drawing;

namespace TagCloudDi_Tests
{
    public class Test_Drawer
    {
        public static Image GetImage(Size size, IEnumerable<Rectangle> rectangles)
        {
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException("size width and height should be positive", "size");
            var image = new Bitmap(size.Width, size.Height);
            using (var gr = Graphics.FromImage(image))
            {
                gr.Clear(Color.Black);
                using (var brush = new SolidBrush(Color.White))
                {
                    foreach (var rectangle in rectangles)
                        gr.FillRectangle(brush, rectangle);
                }
            }
            
            return image;
        }
    }
}
