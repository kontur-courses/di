using System.Collections.Generic;
using Spire.Doc;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class ParserDocx : IParser
    {
        private readonly ParserSettings settings;

        public ParserDocx(ParserSettings settings)
        {
            this.settings = settings;
        }

        public string FileType => "docx";

        public IEnumerable<string> WordParse(string path)
        {
            var document = new Document(path, FileFormat.Docx);

            if (settings.TextType == TextType.OneWordOneLine)
                return ParserHelper.GetTextParagraph(document);
            return ParserHelper.GetAllWordInDocument(document);
        }
    }
}