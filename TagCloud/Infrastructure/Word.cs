using System;
using System.Drawing;

namespace TagCloud.Infrastructure
{
    public class Word
    {
        public string Value { get; }
        public Size WordRectangleSize { get; private set; }
        public float FontSize { get; private set; }
        public int Count { get; private set; }
        public WordClass WordClass
        {
            get
            {
                if (wordClass == null)
                    throw new InvalidOperationException($"Word {Value} doesn't have word class");
                return wordClass.Value;
            }
        }

        private WordClass? wordClass;
        

        public Word(string word, WordClass? wordClass = null)
        {
            Value = word;
            this.wordClass = wordClass;
        }

        public Word SetCount(int count)
        {
            Count = count;
            return this;
        }

        public Word SetSize(Size size)
        {
            WordRectangleSize = size;
            return this;
        }

        public Word SetFontSize(float fontSize)
        {
            FontSize = fontSize;
            return this;
        }

        public Word SetWordClass(WordClass wordClass)
        {
            this.wordClass = wordClass;
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
