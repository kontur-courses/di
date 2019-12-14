using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public class ToLowerFormatter : IElementFormatter
    {
        public string ConvertToUsedForm(string element)
        {
            return element.ToLower();
        }
    }
}