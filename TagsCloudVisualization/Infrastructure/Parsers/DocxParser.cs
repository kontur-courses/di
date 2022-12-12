using System.Collections.Generic;
using Spire.Doc;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class DocxParser : IParser
    {
        private readonly DocumentParserHelper helper;
        private readonly ParserSettings settings;

        public DocxParser(ParserSettings settings)
        {
            helper = new DocumentParserHelper();
            this.settings = settings;
        }

        public string FileType => "docx";

        public IEnumerable<string> WordParse(string path)
        {
            var document = new Document(path, FileFormat.Docx);

            if (settings.TextType == TextType.OneWordOneLine)
                return helper.GetTextParagraph(document);
            return helper.GetAllWordInDocument(document);
        }
    }
}