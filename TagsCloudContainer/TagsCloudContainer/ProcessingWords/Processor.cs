using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CloudDrawing;
using TagsCloudContainer.PreprocessingWords;
using TagsCloudContainer.Reader;

namespace TagsCloudContainer.ProcessingWords
{
    public class Processor : IProcessor
    {
        private readonly IReader reader;
        private readonly IPreprocessingWords preprocessingWords;
        private readonly ICircularCloudDrawing circularCloudDrawing;
        public Processor(
            IReader reader,
            IPreprocessingWords preprocessingWords,
            ICircularCloudDrawing circularCloudDrawing)
        {
            this.reader = reader;
            this.preprocessingWords = preprocessingWords;
            this.circularCloudDrawing = circularCloudDrawing;
        }
        
        public Bitmap Run(string pathToFile, Color colorBackground,
            string famyilyNameFont, Brush brushText, StringFormat stringFormatText, Size size)
        {
            circularCloudDrawing.SetOptions(colorBackground, size);
            foreach (var frequencyOfWord in GetWordAndNumberOfRepetitions(preprocessingWords.Preprocessing(reader.GetWordsSet(pathToFile))))
                circularCloudDrawing.DrawString(frequencyOfWord.Item1, 
                    new Font( famyilyNameFont, frequencyOfWord.Item2 + 10),
                    brushText, stringFormatText);
            return circularCloudDrawing.GetBitmap();
        }
        
        public static IEnumerable<(string, int)> GetWordAndNumberOfRepetitions(IEnumerable<string> words)
        {
            var frequencyOfWords = new Dictionary<string, int>();
            foreach (var word in words) 
                frequencyOfWords[word] = frequencyOfWords.ContainsKey(word) ? frequencyOfWords[word] + 1: 1;
             
            var random = new Random();
            var wordsRandomSort = frequencyOfWords.Keys.ToArray();
             
            for (var i = 0; i < wordsRandomSort.Length - 1; i += 1)
            {
                var swapIndex = random.Next(i, wordsRandomSort.Length);
                if (swapIndex == i) continue;
                var temp = wordsRandomSort[i];
                wordsRandomSort[i] = wordsRandomSort[swapIndex];
                wordsRandomSort[swapIndex] = temp;
            }
            
            foreach (var word in wordsRandomSort)
                yield return (word, frequencyOfWords[word]);
        }


    }
}