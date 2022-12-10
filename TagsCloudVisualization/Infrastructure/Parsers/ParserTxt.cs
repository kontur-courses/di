using System.Collections.Generic;
using System.IO;
using System.Text;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class ParserTxt : IParser
    {
        private readonly ParserSettings settings;

        public ParserTxt(ParserSettings settings)
        {
            this.settings = settings;
        }

        public string FileType => "txt";

        public IEnumerable<string> WordParse(string path)
        {
            var encoding = Encoding.GetEncoding(settings.Encoding.ToString());

            if (settings.TextType == TextType.OneWordOneLine)
                foreach (var line in File.ReadAllLines(path, encoding))
                    yield return line;

            foreach (string word in ParserHelper.AllWordRegex.Matches(File.ReadAllText(path, encoding)))
                yield return word;
        }
    }
}