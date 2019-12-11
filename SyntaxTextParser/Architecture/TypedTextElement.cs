using System;

namespace SyntaxTextParser.Architecture
{
    public class TypedTextElement
    {
        public readonly string Word;
        public readonly string Type;

        public TypedTextElement(string word, string type, IElementFormatter elementFormatter)
        {
            if(elementFormatter == null)
                throw new ArgumentException($"{nameof(elementFormatter)} can't be null");

            if(word == null)
                throw new ArgumentException("Element can't be null");

            Word = elementFormatter.ConvertToUsedForm(word) 
                   ?? throw new ArgumentException("Element after formatting can't be null");
            Type = type ?? throw new ArgumentException("Element type can't be null");
        }

        public TextElement ConvertToTextElement(int count) =>  new TextElement(Word, count);
    }
}
