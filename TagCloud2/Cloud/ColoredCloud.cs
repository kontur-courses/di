using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;

namespace TagCloud2
{
    public class ColoredCloud : IColoredCloud
    {
        List<IColoredSizedWord> coloredSizedWords = new();

        public List<IColoredSizedWord> GetColoredWords()
        {
            return coloredSizedWords;
        }

        public IColoredCloud GetFromCloudLayouter(string[] words, ICloudLayouter cloud, IColoringAlgorithm coloringAlgorithm, Font font)
        {
            var rectangles = cloud.GetRectangles().ToList();
            for (int i = 0; i < words.Length; i++)
            {
                var color = coloringAlgorithm.GetColor(rectangles[i]);
                coloredSizedWords.Add(new ColoredSizedWord(color, rectangles[i], words[i], font));
            }
            
            return this;
        }
    }
}
