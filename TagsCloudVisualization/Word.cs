using System;

namespace TagsCloudVisualization
{
    public class Word
    {
        public string WordText { get; }

        public Word(string word)
        {
            if (word.Split(' ').Length != 1)
                throw new ArgumentException();
            WordText = word;
        }

        public Word ToLower()
        {
            return new Word(WordText.ToLower());
        }
    }
}
