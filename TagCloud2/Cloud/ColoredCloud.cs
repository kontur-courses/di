using System.Collections.Generic;
using System.Linq;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;

namespace TagCloud2
{
    public class ColoredCloud : IColoredCloud
    {
        public List<IColoredSizedWord> ColoredWords { get; }

        public ColoredCloud()
        {
            ColoredWords = new();
        }

        public void AddColoredWordsFromCloudLayouter(IColoredSizedWord[] words, ICloudLayouter cloud, IColoringAlgorithm coloringAlgorithm)
        {
            var rectangles = cloud.GetRectangles().ToList();
            for (int i = 0; i < words.Length; i++)
            {
                var color = coloringAlgorithm.GetColor(rectangles[i]);
                ColoredWords.Add(new ColoredSizedWord(color, rectangles[i], words[i].Word, words[i].Font));
            }
        }
    }
}
