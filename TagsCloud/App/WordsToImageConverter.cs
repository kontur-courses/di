using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloud.App
{
    class WordsToImageConverter
    {
        public Size ConvertWordToSize(string word, Font font)
        {
            var floatSize = Graphics.FromImage(new Bitmap(10, 10)).MeasureString(word, font);
            return new Size((int)Math.Ceiling((decimal)floatSize.Width), (int)Math.Ceiling((decimal)floatSize.Height));
        }
    }
}
