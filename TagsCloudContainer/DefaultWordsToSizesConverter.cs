using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class DefaultWordsToSizesConverter : IWordsToSizesConverter
    {
        public Size Size { get; set; }
        public int MaxHeight { get; set; }
        public int MaxWidth { get; set; }
        //private readonly  Dictionary<string, int> dictionary;

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
        }

        private Size GetSizeOf(string word, Dictionary<string, int> dictionary)
        {
            var heightL = Size.Height;
            var widthL = Size.Width;
            double width = Math.Sqrt((heightL - heightL/3) * (widthL - widthL/3) * ((double)dictionary[word] / dictionary.Count));
            double height = Math.Sqrt((heightL - heightL/3) * (widthL - widthL/3) * ((double)dictionary[word] / dictionary.Count));
            return new Size(Math.Min((int)width, MaxWidth), 
                Math.Min((int)height, MaxHeight));
        }

        public IEnumerable<(string, Size)> GetSizesOf(Dictionary<string, int> dictionary)
        {
            var res = new List<(string, Size)>();
            foreach (var key in dictionary.Keys)
            {
                var tup = (key, GetSizeOf(key, dictionary));
                res.Add(tup);
            }
            return res;
        }
    }
}