using System;

namespace TagsCloudVisualizationDI
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
    }
}
