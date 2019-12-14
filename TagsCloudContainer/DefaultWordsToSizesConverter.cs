using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class DefaultWordsToSizesConverter : IWordsToSizesConverter
    {
        public Size SizeOfLayout { get; set; }
        public int MaxHeight { get; set; }
        public int MaxWidth { get; set; }
        private readonly  IDictionary<string, int> dict;

        public DefaultWordsToSizesConverter(Size sizeOfLayout, Dictionary<string, int> dictionary, int maxHeight = 0, int maxWidth = 0)
        {
            SizeOfLayout = sizeOfLayout;
            dict = dictionary;
            if (maxHeight == 0)
                MaxHeight = sizeOfLayout.Height;
            if (maxWidth == 0)
                MaxWidth = sizeOfLayout.Width;
            if (maxHeight != 0 && maxWidth != 0)
            {
                MaxHeight = maxHeight;
                MaxWidth = maxWidth;
            }
        }

        public Size GetSizeOf(string word)
        {
            var heightL = SizeOfLayout.Height;
            var widthL = SizeOfLayout.Width;
            double width = Math.Sqrt((heightL - heightL/3) * (widthL - widthL/3) * ((double)dict[word] / dict.Count));
            double height = Math.Sqrt((heightL - heightL/3) * (widthL - widthL/3) * ((double)dict[word] / dict.Count));
            return new Size(Math.Min((int)width, MaxWidth), 
                Math.Min((int)height, MaxHeight));
        }

        public IEnumerable<(string, Size)> GetSizesOf()
        {
            var res = new List<(string, Size)>();
            foreach (var key in dict.Keys)
            {
                var tup = (key, GetSizeOf(key));
                res.Add(tup);
            }
            return res;
        }
    }
}