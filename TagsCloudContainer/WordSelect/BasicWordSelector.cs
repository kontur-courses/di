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
            var wordsFrequency = GetWordsFrequency();

            foreach (var (word, frequency) in wordsFrequency)
            {
                var font = new Font(wordSetting.FontName, frequency < 12 ? frequency + 6 : 18);
                var size = GetSize(word, frequency, font);
                var brush = new SolidBrush(GetColor());
                yield return new LayoutWord(word, brush, font, size);
            }
        }

        private Color GetColor() =>
            wordSetting.Color == "random"
                ? Color.FromArgb(random.Next(255), random.Next(255), random.Next(255))
                : Color.FromName(wordSetting.Color);


        private Dictionary<string, int> GetWordsFrequency()
        {
            var wordsFrequency = new Dictionary<string, int>();
            foreach (var word in reader.ReadWords())
            {
                var clearWord = SelectWord(word);
                if (clearWord is null)
                    continue;
                if (wordsFrequency.ContainsKey(clearWord))
                    wordsFrequency[clearWord]++;
                else
                    wordsFrequency[clearWord] = 1;
            }

            return wordsFrequency;
        }

        private Size GetSize(string word, int frequency, Font font)
        {
            return new Size(word.Length * ((int) Math.Floor(font.Size) - (frequency == 1 ? 0 : 2)),
                font.Height);
        }

        private string SelectWord(string word)
        {
            return word.Length < 4 ? null : word.ToLower().Trim();
        }
    }
}