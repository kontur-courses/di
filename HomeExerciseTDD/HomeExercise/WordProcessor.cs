using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HomeExerciseTDD
{
    public class WordsProcessor : IWordProcessor
    {
        public List<Word> WordsHandle(Dictionary<string, int> words, FontFamily fontFamily)
        {
            return words
                .Select(w => WordHandle(w.Key, w.Value, fontFamily))
                .ToList();
        }
        
        private Word WordHandle(string text, int frequency, FontFamily fontFamily)//WordSettings?
        {
            return new Word(text, frequency, fontFamily);
        }
    }
}