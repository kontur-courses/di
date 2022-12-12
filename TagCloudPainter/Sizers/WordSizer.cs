using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudPainter.Interfaces;

namespace TagCloudPainter.Sizers
{
    public class WordSizer : IWordSizer
    {
        public Size GetTagSize(string word, int count)
        {
            var width = word.Length * 7;
            var height = width / 3;
            return new Size(width, height);
        }
    }
}
