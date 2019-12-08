using System;

namespace SyntaxTextParser
{
    public class CountedTextElement
    {
        public readonly string Element;
        public readonly string SyntaxType;
        public readonly int Count;

        public CountedTextElement(string element, string syntaxType, int count)
        {
            if(count < 0)
                throw new ArgumentOutOfRangeException();

            Element = element ?? throw new NullReferenceException($"{nameof(Element)} can't be null");
            SyntaxType = syntaxType ?? throw new NullReferenceException($"{nameof(SyntaxType)} can't be null");
            Count = count;
        }
    }
}