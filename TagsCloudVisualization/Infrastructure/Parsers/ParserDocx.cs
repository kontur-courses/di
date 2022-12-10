using System.Collections.Generic;
using Spire.Doc;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class ParserDocx : IParser
    {
        private readonly ParserSettings settings;
        private readonly ICurrentTextFileProvider fileProvider;

        public ParserDocx(ParserSettings settings, ICurrentTextFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
            this.settings = settings;
        }

        public string FileType => "docx";

        public IEnumerable<string> WordParse()
        {
            var document = new Document(fileProvider.Path, FileFormat.Docx);

            if (settings.TextType == TextType.OneWordOneLine)
                return ParserHelper.GetTextParagraph(document);
            return ParserHelper.GetAllWordInDocument(document);
        }
    }
}