using TagsCloudVisualization.Styling.TagColorizer;
using TagsCloudVisualization.Styling.TagSizeCalculators;
using TagsCloudVisualization.Styling.Themes;

namespace TagsCloudVisualization.Styling
{
    public class Style
    {
        public ITheme Theme { get; }
        public FontProperties FontProperties { get; }
        public TagSizeCalculator TagSizeCalculator { get; }
        public ITagColorizer TagColorizer { get; }

        public Style(ITheme theme, FontProperties fontProperties, TagSizeCalculator tagSizeCalculator,
            ITagColorizer tagColorizer)
        {
            Theme = theme;
            FontProperties = fontProperties;
            TagSizeCalculator = tagSizeCalculator;
            TagColorizer = tagColorizer;
        }
    }
}