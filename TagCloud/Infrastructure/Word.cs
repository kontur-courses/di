using System.Drawing;

namespace TagCloud.Infrastructure
{
    public class Word
    {
        public string Value { get; }
        public Size WordRectangleSize { get; private set; }
        public int Count { get; private set; }

        public Word(string word)
        {
            Value = word;
        }

        public Size GetSmallestPossibleSize()
        {
            return new Size(Value.Length, 1);
        }

        public Word WithCount(int count)
        {
            Count = count;
            return this;
        }

        public Word WithSize(Size size)
        {
            WordRectangleSize = size;
            return this;
        }

        protected bool Equals(Word other)
        {
            return Value == other.Value 
                   && WordRectangleSize.Equals(other.WordRectangleSize) 
                   && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Word)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Value != null ? Value.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ WordRectangleSize.GetHashCode();
                hashCode = (hashCode * 397) ^ Count;
                return hashCode;
            }
        }
    }
}
