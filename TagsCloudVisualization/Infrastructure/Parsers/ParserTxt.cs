using System.Collections.Generic;
using System.IO;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class ParserTxt : IParser
    {
        private readonly ICurrentTextFileProvider fileProvider;
        private readonly ParserSettings settings;

        public ParserTxt(ParserSettings settings, ICurrentTextFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
            this.settings = settings;
        }

        public string FileType => "txt";

        public IEnumerable<string> WordParse()
        {
            var encoding = ParserHelper.Encodings[settings.Encoding];
            var path = fileProvider.Path;
            if (settings.TextType == TextType.OneWordOneLine)
                foreach (var line in File.ReadAllLines(path, encoding))
                    yield return line;

            foreach (var word in ParserHelper.AllWordRegex.Matches(File.ReadAllText(path, encoding)))
                yield return word.ToString();
        }
    }
}