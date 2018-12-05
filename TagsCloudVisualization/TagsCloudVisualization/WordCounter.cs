using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

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

        public void SetBoringWords(IEnumerable<string> words)
        {
            boringWords = words.ToList();
        }

        public void RemoveBoringWords(IEnumerable<string> words)
        {
            foreach (var word in words)
                boringWords.Remove(word);
        }

        public List<GraphicWord> Count(string rawFile)
        {
            var words = Regex.Split(rawFile.ToLower(), @"\W+");
            var dict = new Dictionary<string, GraphicWord>();
            foreach (var word in words)
            {
                if (!dict.ContainsKey(word))
                    dict[word] = new GraphicWord(word);
                else
                    dict[word].Rate++;
            }

            return dict
                .Values
                .Where(w => w.Rate > 1 && w.Value.Length > 2 && !boringWords.Contains(w.Value))
                .OrderByDescending(w => w.Rate).ToList();
        }
    }
}
