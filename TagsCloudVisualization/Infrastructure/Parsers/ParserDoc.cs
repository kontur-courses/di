using System.Collections.Generic;
using Spire.Doc;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class ParserDoc : IParser
    {
        private readonly ParserSettings settings;

        public ParserDoc(ParserSettings settings)
        {
            this.settings = settings;
        }

        public string FileType => "doc";

        public IEnumerable<string> WordParse(string path)
        {
            var document = new Document(path, FileFormat.Doc);

            if (settings.TextType == TextType.OneWordOneLine)
                return ParserHelper.GetTextParagraph(document);
            return ParserHelper.GetAllWordInDocument(document);
        }
    }
}