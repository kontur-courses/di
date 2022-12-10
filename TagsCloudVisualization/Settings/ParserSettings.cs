using System.Text;
using TagsCloudVisualization.Infrastructure.Parsers;

namespace TagsCloudVisualization.Settings
{
    public class ParserSettings
    {
        public EncodingEnum Encoding { get; set; } = EncodingEnum.UTF8;
        public TextType TextType { get; set; } = TextType.OneWordOneLine;
    }


    public enum EncodingEnum
    {
        UTF8,
        UTF32,
        Unicode,

    }
}