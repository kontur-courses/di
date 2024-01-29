using System.Drawing;

namespace TagsCloudContainer.Drawer
{
    public static class Visualizer
    {

        public static Image Draw(Size size, IEnumerable<TextImage> textImages)
        {
            var image = new Bitmap(size.Width, size.Height);
            var gr = Graphics.FromImage(image);

            var brush = new SolidBrush(Color.White);

            gr.Clear(Color.Black);

            foreach (var textImage in textImages)
            {
                gr.DrawString(textImage.Text, textImage.Font, brush, textImage.Position);
            }

            return image;
        }
    }
}