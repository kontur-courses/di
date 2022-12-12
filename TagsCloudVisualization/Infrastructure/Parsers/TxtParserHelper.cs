using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class TxtParserHelper : IParserHelper
    {
        public Dictionary<EncodingEnum, Encoding> Encodings { get; } = new()
        {
            [EncodingEnum.Unicode] = Encoding.Unicode,
            [EncodingEnum.Utf32] = Encoding.UTF32,
            [EncodingEnum.Utf8] = Encoding.UTF8
        };

        public Regex SelectAllWordsRegex => new(@"([\w]+)", RegexOptions.Compiled);
    }
}