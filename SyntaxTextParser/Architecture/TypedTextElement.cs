using System;

namespace SyntaxTextParser.Architecture
{
    public class TypedTextElement
    {
        public readonly string Word;
        public readonly string Type;

        public TypedTextElement(string word, string type)
        {
            Word = word ?? throw new ArgumentException("Element can't be null");
            Type = type ?? throw new ArgumentException("Element type can't be null");
        }

        public TextElement ConvertToTextElement(int count, IElementPreformer format) => 
            new TextElement(format.ConvertToUsedForm(Word), count);
    }
}
