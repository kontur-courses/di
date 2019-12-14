using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public class LengthMoreThanValidator : IElementValidator
    {
        public bool IsValidElement(TypedTextElement element)
        {
            return element.Word.Length > 1;
        }
    }
}