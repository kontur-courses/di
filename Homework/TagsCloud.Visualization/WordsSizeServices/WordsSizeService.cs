using System;
using System.Drawing;
using TagsCloud.Visualization.FontFactory;
using TagsCloud.Visualization.Models;

namespace TagsCloud.Visualization.WordsSizeServices
{
    public class WordsSizeService : IWordsSizeService
    {
        public Size CalculateSize(Word word, FontDecorator fontDecorator)
        {
            using var graphics = Graphics.FromHwnd(IntPtr.Zero);
            using var font = fontDecorator.Build();
            return Size.Ceiling(graphics.MeasureString(word.Content, font));
        }
    }
}