using System.Collections.Generic;
using System.Text.RegularExpressions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public class DocumentParserHelper : IParserHelper
    {
        public Regex SelectAllWordsRegex => new(@"([\w]+)", RegexOptions.Compiled);

        public IEnumerable<string> GetTextParagraph(IDocument document)
        {
            foreach (Section section in document.Sections)
            foreach (Paragraph paragraph in section.Paragraphs)
                yield return paragraph.Text;
        }

        public IEnumerable<string> GetAllWordInDocument(IDocument document)
        {
            foreach (var paragraph in GetTextParagraph(document))
            foreach (var word in SelectAllWordsRegex.Matches(paragraph))
                yield return word.ToString();
        }
    }
}