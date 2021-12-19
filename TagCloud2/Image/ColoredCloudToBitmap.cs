using System.Drawing;

namespace TagCloud2.Image
{
    public class ColoredCloudToBitmap : IColoredCloudToImageConverter
    {
        public System.Drawing.Image GetImage(IColoredCloud cloud, int xSize, int ySize)
        {
            var words = cloud.ColoredWords;
            var bitmap = new Bitmap(xSize, ySize);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Black);
            foreach (var coloredSizedWord in words)
            {
                var word = coloredSizedWord.Word;
                var brush = new SolidBrush(coloredSizedWord.Color);
                graphics.DrawString(word, coloredSizedWord.Font, brush, coloredSizedWord.Size);
            }
            return bitmap;
        }
    }
}
