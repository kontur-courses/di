using System.Collections.Generic;
using Spire.Doc;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class ParserDoc : IParser
    {
        private readonly ParserSettings settings;
        private readonly ICurrentTextFileProvider fileProvider;

        public ParserDoc(ParserSettings settings, ICurrentTextFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
            this.settings = settings;
        }

        public string FileType => "doc";

        public IEnumerable<string> WordParse()
        {
            var document = new Document(fileProvider.Path, FileFormat.Doc);
            return settings.TextType == TextType.OneWordOneLine
                ? ParserHelper.GetTextParagraph(document)
                : ParserHelper.GetAllWordInDocument(document);
        }
    }
}