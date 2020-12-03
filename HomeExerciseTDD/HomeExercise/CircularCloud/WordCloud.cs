using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HomeExerciseTDD.Helpers;
using HomeExerciseTDD.settings;

namespace HomeExerciseTDD
{
    public class WordCloud : IWordCloud
    {
        private readonly IWordsProcessor wordsProcessor;
        public List<ISizedWord> SizedWords { get; }
        private ICircularCloudLayouter layouter;
        public Point Center { get; private set; }

        public WordCloud(ICircularCloudLayouter layouter,  IWordsProcessor wordsProcessor)
        {
            SizedWords = new List<ISizedWord>();
            this.layouter = layouter;
            this.wordsProcessor = wordsProcessor;
        }

       public void BuildCloud()
       {
           var words = wordsProcessor.WordsHandle();
           foreach (var word in words)
            {
                var rectangle = GetRectangle(word, word.Size);
                Console.WriteLine($"Height:{rectangle.Size.Height}{Environment.NewLine}Width:{rectangle.Size.Width}");
                SizedWords.Add(new SizedWord(word, word.Size, word.Font, rectangle));
            }

            Center = SizedWords.First().Rectangle.Location;
        }

        private Rectangle GetRectangle(IWord word, int size)
        {
            return layouter
                .PutNextRectangle(GraphicsHelper.MeasureString(word.Text, new Font(word.Font, size)));
        }
        
               
        private Size ToEnlarge(Size size, int coefficient)
        {
            var resultSize = size;
            resultSize.Height += coefficient;
            resultSize.Width += coefficient;

            return resultSize;
        }
    }
}