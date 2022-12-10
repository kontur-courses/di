using TagsCloudVisualization.Infrastructure.Parsers;

namespace TagsCloudVisualization.Settings
{
    public class ParserSettings
    {
        public EncodingEnum Encoding { get; set; } = EncodingEnum.Utf8;

        public TextType TextType { get; set; } = TextType.LiteraryText;
    }
}