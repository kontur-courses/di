using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public static class TagsCloudVisualizer
    {
        private const int EdgeLength = 150;

        public static Bitmap GetPicture(List<CloudWordData> data, CloudParameters parameters)
        {
            var width = parameters.ImageSize.Value.Width;
            var height = parameters.ImageSize.Value.Height;
            var color = parameters.Color;
            var fontName = parameters.FontName;
            var bitmap = new Bitmap(width + EdgeLength, height + EdgeLength);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                var horizontalOffset = (float) (width + EdgeLength) / 2;
                var verticalOffset = (float) height / 2 + (float) EdgeLength * 3 / 4;
                graphics.TranslateTransform(horizontalOffset, verticalOffset);

                foreach (var wordData in data)
                    graphics.DrawString(wordData.Word, new Font(fontName, wordData.Weight * 14),
                        new SolidBrush((Color) color),
                        wordData.StartPoint);
            }

            return bitmap;
        }
    }
}