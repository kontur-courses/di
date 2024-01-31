using System.Drawing;

namespace TagsCloudContainer.Drawer
{
    public static class Visualizer
    {

        public static Image Draw(Size size, IEnumerable<TextImage> textImages, Color? bgColor = null)
        {
            var image = new Bitmap(size.Width, size.Height);
            var gr = Graphics.FromImage(image);

            gr.Clear(bgColor ?? Color.Black);

            foreach (var textImage in textImages)
            {
                gr.DrawString(textImage.Text, textImage.Font, new SolidBrush(textImage.Color), textImage.Position);
            }

            return image;
        }
    }
}