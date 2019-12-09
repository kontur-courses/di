using TagsCloudVisualization.Styling.TagSizeCalculators;
using TagsCloudVisualization.Styling.Themes;

namespace TagsCloudVisualization.Styling
{
    public class Style
    {
        public Theme Theme { get; }
        public FontProperties FontProperties { get; }
        public TagSizeCalculator TagSizeCalculator { get; }

        public Style(Theme theme, FontProperties fontProperties, TagSizeCalculator tagSizeCalculator)
        {
            Theme = theme;
            FontProperties = fontProperties;
            TagSizeCalculator = tagSizeCalculator;
        }
    }
}