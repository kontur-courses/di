using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class RectangleTagsCloudVisualizer
    {
        private const int EdgeLength = 150;

        public static Bitmap GetPicture(Point[] startPoints, List<KeyValuePair<string, int>> sortedWords, Color? color)
        {
            var width = 1024;
            var height = 768;
            var bitmap = new Bitmap(width + EdgeLength, height + EdgeLength);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                var horizontalOffset = (float) (width + EdgeLength) / 2;
                var verticalOffset = (float) height / 2 + (float) EdgeLength * 3 / 4;
                graphics.TranslateTransform(horizontalOffset, verticalOffset);
                var counter = 0;

                foreach (var word in sortedWords)
                {
                    graphics.DrawString(word.Key, new Font("Arial", word.Value * 14), new SolidBrush((Color) color),
                        startPoints[counter]);
                    counter++;
                }
            }

            return bitmap;
        }
    }
}