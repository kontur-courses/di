using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class WordsTransformer : IWordsTransformer
    {
        private IWordSelector selector;
        private IWordReader reader;
        private Setting setting;

        public WordsTransformer(IWordSelector selector, IWordReader reader, Setting setting)
        {
            this.selector = selector;
            this.reader = reader;
            this.setting = setting;
        }

        public IEnumerable<(Size, string)> Transform()
        {
            var wordsRate = new Dictionary<string, int>();
            foreach (var word in reader.ReadWords())
            {
                var clearWord = selector.Select(word);
                if(clearWord is null)
                    continue;
                if (wordsRate.ContainsKey(clearWord))
                    wordsRate[clearWord]++;
                else
                    wordsRate[clearWord] = 1;                
            }
            
            var listOfWords = new List<(Size, string)>();
            foreach (var (word, rate) in wordsRate)
            {
                listOfWords.Add((new Size(word.Length * setting.Font.Height * rate, rate * setting.Font.Height), word));
            }

            return listOfWords;
        }
    }
}