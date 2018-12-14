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
            var brushes = GetBrushes(parameters.ColorFunc, data.Count);
            var fontName = parameters.FontName;
            var bitmap = new Bitmap(width + EdgeLength, height + EdgeLength);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
                var horizontalOffset = (float) (width + EdgeLength) / 2;
                var verticalOffset = (float) height / 2 + (float) EdgeLength * 3 / 4;
                graphics.TranslateTransform(horizontalOffset, verticalOffset);

                foreach (var wordData in data.Select((el, id) => new {element = el, id}))
                    graphics.DrawString(wordData.element.Word, new Font(fontName, wordData.element.Weight * 14),
                        brushes[wordData.id],
                        wordData.element.StartPoint);
            }

            return bitmap;
        }

        private static List<SolidBrush> GetBrushes(Func<float, Color> colorFunc, int dataCount)
        {
            var brushes = new List<SolidBrush>();
            for (var i = 0; i < dataCount; i++)
                brushes.Add(new SolidBrush(colorFunc((float)i/dataCount)));
            return brushes;
        }

        public static void SavePicture(Bitmap picture, ImageFormat format)
        {
            var path = Application.StartupPath;
            picture.Save($"{path}\\CloudTags.{format}", format);
            Console.WriteLine($"Pictures saved in {path}\\CloudTags.{format}");
        }
    }
}