using System;

namespace TagsCloudVisualisation
{
    /// <summary>
    /// Класс слова, который хранит информацию о слове, его количестве встреч в слове и индексе tf
    /// </summary>
    public class Word : IEquatable<Word>
    {
        public string Value { get; }
        public int Count { get; set; }
        public double Tf { get; set; }
        
        public Word(string value, int count = 1, double tf = 1)
        {
            Value = value;
            Count = count;
            Tf = tf;
        }

        public bool Equals(Word? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Word)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}