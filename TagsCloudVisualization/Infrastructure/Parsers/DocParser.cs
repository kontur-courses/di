using System.Collections.Generic;
using Spire.Doc;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class DocParser : IParser
    {
        private readonly DocumentParserHelper helper;
        private readonly ParserSettings settings;

        public DocParser(ParserSettings settings)
        {
            helper = new DocumentParserHelper();
            this.settings = settings;
        }

        public string FileType => "doc";

        public IEnumerable<string> WordParse(string path)
        {
            var document = new Document(path, FileFormat.Doc);
            return settings.TextType == TextType.OneWordOneLine
                ? helper.GetTextParagraph(document)
                : helper.GetAllWordInDocument(document);
        }
    }
}