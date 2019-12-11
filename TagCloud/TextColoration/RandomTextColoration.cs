using System;
using System.Drawing;

namespace TagCloud.TextColoration
{
    public class RandomTextColoration : ITextColoration
    {
        public Brush GetTextColor(string word, int frequency)
        {
            var rnd = new Random(frequency * word.Length);

            var brushesType = typeof(Brushes);

            var properties = brushesType.GetProperties();

            var random = rnd.Next(properties.Length);
            var result = (Brush) properties[random].GetValue(null, null);

            return result;
        }
    }
}