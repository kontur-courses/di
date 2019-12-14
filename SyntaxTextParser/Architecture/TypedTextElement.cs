using System;

namespace SyntaxTextParser.Architecture
{
    public class TypedTextElement : IEquatable<TypedTextElement>
    {
        public readonly string Word;
        public readonly string Type;

        public TypedTextElement(string word, string type, IElementFormatter elementFormatter)
        {
            if(elementFormatter == null)
                throw new ArgumentException($"{nameof(elementFormatter)} can't be null");

            if(string.IsNullOrEmpty(word))
                throw new ArgumentException("Element can't be null or empty");

            Word = elementFormatter.ConvertToUsedForm(word) 
                   ?? throw new ArgumentException("Element after formatting can't be null");
            Type = type ?? throw new ArgumentException("Element type can't be null");
        }

        public TextElement ConvertToTextElement(int count) =>  new TextElement(this, count);

        public bool Equals(TypedTextElement other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Word == other.Word && Type == other.Type;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Word != null ? Word.GetHashCode() : 0) * 397) ^ (Type != null ? Type.GetHashCode() : 0);
            }
        }
    }
}
