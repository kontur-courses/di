namespace SyntaxTextParser.Architecture
{
    public interface IElementValidator
    {
        bool IsValidElement(TypedTextElement element);
    }
}