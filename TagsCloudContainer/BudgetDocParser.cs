using GroupDocs.Parser.Options;
using GroupDocs.Parser;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class BudgetDocParser : IDocParser
    {
        public List<string> ParseDoc(string filePath)
        {
            var parser = new Parser(filePath);

            using var reader = parser.GetFormattedText(new FormattedTextOptions(FormattedTextMode.PlainText));
            return reader == null ? throw new ArgumentException("Unsupported file format, or empty file")
                : reader.ReadToEnd().Split("\r\n",StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
