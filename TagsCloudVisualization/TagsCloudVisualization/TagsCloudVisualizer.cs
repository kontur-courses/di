using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public static class TagsCloudVisualizer
    {
        private const int EdgeLength = 150;

        public static Bitmap GetPicture(List<CloudWordData> data, CloudParameters parameters)
        {
            var width = parameters.ImageSize.Width;
            var height = parameters.ImageSize.Height;
            var colors = parameters.Colors.ToArray();
            var fontName = parameters.FontName;
            var bitmap = new Bitmap(width + EdgeLength, height + EdgeLength);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                var horizontalOffset = (float) (width + EdgeLength) / 2;
                var verticalOffset = (float) height / 2 + (float) EdgeLength * 3 / 4;
                graphics.TranslateTransform(horizontalOffset, verticalOffset);

                for (var i = 0; i < data.Count; i++)
                {
                    graphics.DrawString(data[i].Word, new Font(fontName, data[i].Weight * 14),
                        new SolidBrush(colors[i % colors.Length]),
                        data[i].StartPoint);
                }
            }

            return bitmap;
        }

        public static void SavePicture(Bitmap picture, ImageFormat format)
        {
            var path = $"{Application.StartupPath}";
            picture.Save($"{path}\\CloudTags.{format}", format);
            Console.WriteLine($"Pictures saved in {path}\\CloudTags.{format}");
        }
    }
}