using System.Collections.Generic;
using System.IO;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class TxtParser : IParser
    {
        private readonly TxtParserHelper helper;
        private readonly ParserSettings settings;

        public TxtParser(ParserSettings settings)
        {
            helper = new TxtParserHelper();
            this.settings = settings;
        }

        public string FileType => "txt";

        public IEnumerable<string> WordParse(string path)
        {
            var encoding = helper.Encodings[settings.Encoding];
            if (settings.TextType == TextType.OneWordOneLine)
                foreach (var line in File.ReadLines(path, encoding))
                    yield return line;
            else
                foreach (var word in helper
                             .SelectAllWordsRegex
                             .Matches(File.ReadAllText(path, encoding)))
                    yield return word.ToString();
        }
    }
}