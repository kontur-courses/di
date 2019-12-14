using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NHunspell;
using TagsCloudContainer.Infrastructure.Common;

namespace TagsCloudContainer
{
    public class BasicWordsSelector : IWordsSelector
    {
        private IWordReader reader;
        private WordSetting wordSetting;
        private Random random = new Random();

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
                var size = new Size(x.Length * ((int) Math.Floor(font.Size) - (layoutWords[x] == 1 ? 0 : 2)),
                    font.Height);
                Color color;
                color = wordSetting.Color == "random"
                    ? Color.FromArgb(random.Next(255), random.Next(255), random.Next(255))
                    : Color.FromName(wordSetting.Color);
                var brush = new SolidBrush(color);
                return new LayoutWord(x, brush, font, size);
            });
        }

        private string SelectWord(string word)
        {
            return word.Length < 4 ? null : word.ToLower().Trim();
        }
    }
}