using System;

namespace SyntaxTextParser
{
    public class CountedTextElement
    {
        public readonly string Word;
        public readonly string SyntaxType;
        public readonly int Count;

        public CountedTextElement(string word, string syntaxType, int count)
        {
            if(count < 0)
                throw new ArgumentOutOfRangeException();

            Word = word ?? throw new NullReferenceException($"{nameof(Word)} can't be null");
            SyntaxType = syntaxType ?? throw new NullReferenceException($"{nameof(SyntaxType)} can't be null");
            Count = count;
        }
    }
}