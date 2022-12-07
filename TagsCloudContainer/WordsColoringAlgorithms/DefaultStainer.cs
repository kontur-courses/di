using System.Drawing;

namespace TagsCloudContainer.WordsColoringAlgorithms
{

    public class DefaultStainer : IWordStainer
    {
        public int WordsCount { get; }
        public Color StartColor { get; }
        

        public DefaultStainer(InputFileHandler handler, CustomSettings settings)
        {
            StartColor = Color.FromName(settings.BrushColor);
            WordsCount = handler.FormFrequencyDictionary().Count;
        }
        public Color[] GetColorsSequence()
        {
            var colors = new Color[WordsCount];
            for (int i = 0; i < WordsCount; i++)
                colors[i] = StartColor;
            return colors;
        }
    }

}