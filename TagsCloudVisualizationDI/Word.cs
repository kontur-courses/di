using System;
using TagsCloudVisualizationDI.TextAnalization;

namespace TagsCloudVisualizationDI
{
    public class Word
    {
        public string WordText { get; set; }
        public int CntOfWords { get; set; }

        public PartsOfSpeech.SpeechPart Type { get; }

        public Word(string word, PartsOfSpeech.SpeechPart type)
        {
            if (word.Split(' ').Length != 1)
                throw new ArgumentException();
            WordText = word;
            CntOfWords = 1;
            Type = type;
        }
    }
}
