using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using NHunspell;

namespace TagsCloudVisualization
{
    /// <summary>
    /// Счетчик слов, который отсекает слова, встречающиеся один раз
    /// </summary>
    public class WordCounter
    {
        public Font Font { get; set; } = new Font("Consolas", 14);
        private List<string> stopWords;

        public WordCounter()
        {
            try
            {
                stopWords = Regex.Split(new TxtReader().Read("Stopwords.txt").ToLower(), @"\W+").ToList();
            }
            catch
            {
                stopWords = new List<string>();
            }
        }

        private List<string> SpellCheck(List<string> words)
        {
            var checkedWords = new List<string>();
            using (var hunspell = new Hunspell("ru.aff", "ru.dic"))
            {
                foreach (var word in words)
                {
                    var variants = hunspell.Stem(word);
                    checkedWords.Add(variants.Count == 0 ? word : variants.First());
                }
            }
            return checkedWords;
        }

        public List<GraphicWord> Count(bool withSpellCheck=true, params string[] row)
        {
            var words = new List<string>();
            foreach (var s in row)
            {
                words.AddRange(Regex.Split(s.ToLower(), @"\W+"));
            }
            var countedWords = new Dictionary<string, GraphicWord>();
            if (withSpellCheck)
                words = SpellCheck(words);
            foreach (var word in words)
            {
                if (!countedWords.ContainsKey(word))
                    countedWords[word] = new GraphicWord(word);
                else
                    countedWords[word].Rate++;
            }

            foreach (var dictValue in countedWords.Values)
            {
                dictValue.Font = new Font(Font.Name, dictValue.Rate + Font.Size);
            }

            return countedWords
                .Values
                .Where(w => w.Rate > 1 && !stopWords.Contains(w.Value))
                .OrderByDescending(w => w.Rate).ToList();
        }
    }
}
