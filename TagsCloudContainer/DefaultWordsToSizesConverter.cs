using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class DefaultWordsToSizesConverter : IWordsToSizesConverter
    {
        public Size Size { get; set; }
        public int MaxHeight { get; set; }
        public int MaxWidth { get; set; }
        private Graphics graphics;

        public DefaultWordsToSizesConverter(Size size, int maxHeight = 0, int maxWidth = 0)
        {
            Size = size;
            if (maxHeight == 0)
                MaxHeight = size.Height;
            if (maxWidth == 0)
                MaxWidth = size.Width;
            if (maxHeight != 0 && maxWidth != 0)
            {
                MaxHeight = maxHeight;
                MaxWidth = maxWidth;
            }

            var bitmap = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bitmap);
            this.graphics = g;
        }

        private Size GetSizeOf(string word, Dictionary<string, int> dictionary)
        {
            var count = dictionary.Select(x => x.Value).Sum();
            var sizeFD = graphics.MeasureString(word, new Font("Tahoma", 500));
            var widthFD = sizeFD.Width * ((double) dictionary[word] / count);
            var heightFD = sizeFD.Height * ((double) dictionary[word] / count);
            return new Size(Math.Min((int) widthFD, MaxWidth),
                Math.Min((int) heightFD, MaxHeight));
        }

        public IEnumerable<(string, Size)> GetSizesOf(Dictionary<string, int> dictionary)
        {
            var res = new List<(string, Size)>();
            foreach (var key in dictionary.Keys)
            {
                var tup = (key, GetSizeOf(key, dictionary));
                res.Add(tup);
            }

            var ourSquare = 0.0;
            foreach (var item in res)
            {
                var rect = item.Item2;
                ourSquare += rect.Height * rect.Width;
            }

            double bitmapSquare = (Size.Height - Size.Height / 2.5) * (Size.Width - Size.Width / 2.5);

            var coeff = Math.Sqrt(bitmapSquare / ourSquare);

            var result = new List<(string, Size)>();
            foreach (var item in res)
            {
                var newRect = new Size((int) (item.Item2.Width * coeff), (int) (item.Item2.Height * coeff));
                result.Add((item.Item1, newRect));
            }

            return result;
        }
    }
}