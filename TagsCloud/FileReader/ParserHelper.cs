using System.Text.RegularExpressions;
using Spire.Doc.Interface;

namespace TagsCloud;

public static class ParserHelper
{
    private static Regex SelectAllWordsRegex => new(@"([\w]+)", RegexOptions.Compiled);

    public static IEnumerable<string?> GetTextParagraph(IDocument document)
    {
        var section = document.Sections[0];
        for (var i = 0; i < section.Paragraphs.Count; i++)
            foreach (var word in SelectAllWordsRegex.Matches(section.Paragraphs[i].Text))
                yield return word.ToString().Trim().ToLower();
    }
}