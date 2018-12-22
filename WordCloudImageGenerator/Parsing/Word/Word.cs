using System.Collections.Generic;

namespace WordCloudImageGenerator.Parsing.Word
{
    public class Word : IWord
    {
        public string Text { get; }
        public int Entries { get; }

        public Word(KeyValuePair<string, int> textOccurrencesPair)
            : this(textOccurrencesPair.Key, textOccurrencesPair.Value)
        {
        }

        public Word(string text, int entries)
        {
            Text = text;
            Entries = entries;
        }

        public int CompareTo(IWord other)
        {
            return Entries - other.Entries;
        }
    }
}