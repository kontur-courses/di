using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization.WordPainting
{
    public class WordClassBasedWordPainter : IWordPainter
    {
        private readonly PictureConfig pictureConfig;
        private Color[] Colors => pictureConfig.Palette.WordsColors;

        private readonly Dictionary<WordClass, Color> wordClassesColors = new Dictionary<WordClass, Color>();
        private int currentNewColorIndex;

        public WordClassBasedWordPainter(PictureConfig pictureConfig)
        {
            this.pictureConfig = pictureConfig;
        }

        public Color GetWordColor(Word word)
        {
            if (wordClassesColors.TryGetValue(word.WordClass, out var color))
                return color;
            var nextColor = currentNewColorIndex >= Colors.Length
                ? new Color().GetRandomColor()
                : Colors[currentNewColorIndex];
            currentNewColorIndex++;
            wordClassesColors[word.WordClass] = nextColor;
            return nextColor;
        }

        public string Name => "class";
    }
}
