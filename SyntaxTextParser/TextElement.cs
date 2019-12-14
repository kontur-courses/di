using System;
using SyntaxTextParser.Architecture;

namespace SyntaxTextParser
{
    public class TextElement
    {
        public readonly string Element;
        public readonly int Count;

        public TextElement(TypedTextElement element, int count)
        {
            if(count < 0)
                throw new ArgumentOutOfRangeException($"Element count can't be less than zero");

            Element = element.Word;
            Count = count;
        }
    }
}