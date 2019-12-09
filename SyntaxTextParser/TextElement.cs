using System;

namespace SyntaxTextParser
{
    public class TextElement
    {
        public readonly string Element;
        public readonly int Count;

        public TextElement(string element, int count)
        {
            if(count < 0)
                throw new ArgumentOutOfRangeException();

            Element = element ?? throw new NullReferenceException($"{nameof(Element)} can't be null");
            Count = count;
        }
    }
}