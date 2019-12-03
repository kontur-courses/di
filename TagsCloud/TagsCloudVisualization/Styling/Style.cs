using TagsCloudVisualization.Styling.WordSizeCalculators;
using TagsCloudVisualization.Styling.Themes;

namespace TagsCloudVisualization.Styling
{
    public class Style
    {
        public Theme Theme { get; }
        public FontProperties FontProperties { get; }
        public WordSizeCalculator WordSizeCalculator { get; }

        public Style(Theme theme, FontProperties fontProperties, WordSizeCalculator wordSizeCalculator)
        {
            Theme = theme;
            FontProperties = fontProperties;
            WordSizeCalculator = wordSizeCalculator;
        }
        
        public float GetWordSize(int wordCount)
        {
            return WordSizeCalculator.GetScaleFactor(wordCount) * FontProperties.MinSize;
        }
    }
}