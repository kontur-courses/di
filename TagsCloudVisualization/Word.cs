using System;

namespace TagsCloudVisualization
{
    public class Word
    {
        public string WordText { get; }
        public int CntOfWords { get; set; }

        public Word(string word)
        {
            if (word.Split(' ').Length != 1)
                throw new ArgumentException();
            WordText = word;
            CntOfWords = 1;
        }

        public Word ToLower()
        {
            return new Word(WordText.ToLower());
        }
    }
}
