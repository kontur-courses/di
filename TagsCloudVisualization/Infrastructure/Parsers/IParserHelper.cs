using System.Text.RegularExpressions;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public interface IParserHelper
    {
        Regex SelectAllWordsRegex { get; }
    }
}