using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.WordsPreprocessing
{
    public class Word
    {
        public string Value { get; }
        public SpeechPart PartOfSpeech { get; }
        public int Count { get; set; }
        public double Frequency { get; set; }


        public Word(string value, SpeechPart speechPart)
        {
            Value = value;
            PartOfSpeech = speechPart;
        }
    }
}
