using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NHunspell;

namespace TagsCloudContainer
{
    public class BasicWordsSelector : IWordsSelector
    {
        private IWordReader reader;
        private WordSetting wordSetting;

        public BasicWordsSelector(IWordReader reader, WordSetting wordSetting)
        {
            this.reader = reader;
            this.wordSetting = wordSetting;
        }

        public IEnumerable<LayoutWord> Select()
        {
            var layoutWords = new Dictionary<string, int>();
            foreach (var word in reader.ReadWords())
            {
                var clearWord = SelectWord(word);
                if (clearWord is null)
                    continue;
                if (layoutWords.ContainsKey(clearWord))
                    layoutWords[clearWord]++;
                else
                    layoutWords[clearWord] = 1;
            }

            return layoutWords.Keys.Select(x =>
            {
                var font = new Font(wordSetting.FontName, layoutWords[x] < 12 ? layoutWords[x] + 6 : 18);
                var size = new Size(x.Length * (int) Math.Round(font.Size), font.Height);
                return new LayoutWord(x, wordSetting.Brush, font, size);
            });
        }

        private string SelectWord(string word)
        {
            return word.Length < 4 ? null : word.ToLower().Trim();
        }
    }
}