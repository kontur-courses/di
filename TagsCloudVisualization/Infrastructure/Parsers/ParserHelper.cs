using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace TagsCloudVisualization.Infrastructure.Parsers
{
    public static class ParserHelper
    {
        public static Regex AllWordRegex => new(@"([\w]+-[\w]+)|([\w]+)");

        public static Dictionary<EncodingEnum, Encoding> Encodings = new()
        {
            [EncodingEnum.Unicode] = Encoding.Unicode,
            [EncodingEnum.Utf32] = Encoding.UTF32,
            [EncodingEnum.Utf8] = Encoding.UTF8
        };


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