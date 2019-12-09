using System.Collections.Generic;

namespace TagsCloudVisualization.Core
{
    public class Word
    {
        public readonly string Value;

        public Word(string value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Word word &&
                   Value == word.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }
    }
}