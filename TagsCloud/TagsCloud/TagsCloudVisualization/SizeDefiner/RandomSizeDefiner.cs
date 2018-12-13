using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloud.TagsCloudVisualization
{
    public class RandomSizeDefiner: ISizeDefiner
    {
        private readonly string fontName;
        private readonly int maxFontSize;
        private readonly int minFontSize;
        private readonly Random random = new Random();

        public RandomSizeDefiner(string fontName, int minFontSize = 10, int maxFontSize = 100)
        {
            this.fontName = fontName;
            this.maxFontSize = maxFontSize;
            this.minFontSize = minFontSize;
        }

        public Tuple<Size,int> GetSizeAndFont(string word, int frequency, int minFrequency, int maxFrequency)
        {
            var fontSize = GetFontSize();
            var size = TextRenderer.MeasureText(word, new Font(fontName, fontSize));
            return Tuple.Create(size, fontSize);
        }

        private int GetFontSize()
        {
            return random.Next(minFontSize, maxFontSize);
        }
    }

}
