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
        private List<string> boringWords = new List<string>
        {
            "Я", "мы", "ты", "вы", "он", "она", "оно", "они",
            "мой", "твой", "наш", "ваш", "свой", "и", "мои", "моего", "моему", "моей", "моим",
            "кто", "что", "какой", "чей", "как", "где", "куда", "когда",
            "а", "у", "в", "на", "за", "под", "к", "около", "перед", "ему", "это", "этот"
        };

        public void AddBoringWords(IEnumerable<string> words)
        {
            boringWords.AddRange(words);
        }

        public void SetBoringWords(List<string> words)
        {
            boringWords = words;
        }

        public void RemoveBoringWords(IEnumerable<string> words)
        {
            foreach (var word in words)
                boringWords.Remove(word);
        }

        public List<GraphicWord> Count(params string[] row)
        {
            var words = new List<string>();
            foreach (var s in row)
            {
                words.AddRange(Regex.Split(s.ToLower(), @"\W+"));
            }
            var checkedWords = new List<string>();
            using (Hunspell hunspell = new Hunspell("ru.aff", "ru.dic"))
            {
                foreach (var word in words)
                {
                    var variants = hunspell.Stem(word);
                    if (variants.Count == 0)
                        checkedWords.Add(word);
                    else
                        checkedWords.Add(variants?.First());

                }
            }
            var countedWords = new Dictionary<string, GraphicWord>();
            foreach (var word in checkedWords)
            {
                if (!countedWords.ContainsKey(word))
                    countedWords[word] = new GraphicWord(word);
                else
                    countedWords[word].Rate++;
            }

            foreach (var dictValue in countedWords.Values)
            {
                dictValue.Font = new Font("Consolas", dictValue.Rate + 8);
            }

            return countedWords
                .Values
                .Where(w => w.Rate > 1 && !boringWords.Contains(w.Value))
                .OrderByDescending(w => w.Rate).ToList();
        }
    }
}
