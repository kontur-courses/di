using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HomeExerciseTDD.settings;

namespace HomeExerciseTDD
{
    public class WordsProcessor : IWordProcessor
    {
        private IFileProcessor fileProcessor;
        private WordSettings settings;
        public WordsProcessor(IFileProcessor fileProcessor, WordSettings settings)
        {
            this.fileProcessor = fileProcessor;
            this.settings = settings;
        }
        
        public  List<Word> WordsHandle()
        {
            var words = fileProcessor.GetWords();
            return words
                .Select(w => WordHandle(w.Key, w.Value))
                .ToList();
        }
   
        private Word WordHandle(string text, int frequency)//WordSettings?
        {
            return new Word(text, frequency, settings.Font, frequency*settings.Coefficient);
        }
        
    }
}