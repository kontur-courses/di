using System;

namespace TagsCloudVisualization
{
    public class Word
    {
        public string WordText { get; set; }
        public int CntOfWords { get; set; }

        public Word(string word)
        {
            if (word.Split(' ').Length != 1)
                throw new ArgumentException();
            WordText = word;
            CntOfWords = 1;
        }

        public void ToLower()
        {
            WordText.ToLower();
        }
    }
}
