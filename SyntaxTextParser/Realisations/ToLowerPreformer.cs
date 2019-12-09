using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public class ToLowerPreformer : IElementPreformer
    {
        public string ConvertToUsedForm(string element)
        {
            return element.ToLower();
        }
    }
}