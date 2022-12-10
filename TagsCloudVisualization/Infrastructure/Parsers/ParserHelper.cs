using System.Collections.Generic;
using System.Text.RegularExpressions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public static class ParserHelper
    {
        public static Regex AllWordRegex => new(@"([\w]+-[\w]+)|([\w]+)");

        public static IEnumerable<string> GetTextParagraph(IDocument document)
        {
            foreach (Section section in document.Sections)
            foreach (Paragraph paragraph in section.Paragraphs)
                yield return paragraph.Text;
        }

        public static IEnumerable<string> GetAllWordInDocument(IDocument document)
        {
            foreach (var paragraph in GetTextParagraph(document))
            foreach (var word in AllWordRegex.Matches(paragraph))
                yield return word.ToString();
        }
    }
}