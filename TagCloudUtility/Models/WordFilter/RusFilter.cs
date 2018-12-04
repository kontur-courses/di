using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Utility.Models.WordFilter
{
    /// <summary>
    /// Class for russian words, deleting all pretexts, pronouns and words with length less than 3, selecting them to lower case
    /// </summary>
    public class RusFilter : IWordFilter
    {
        private readonly HashSet<string> stopWords = new HashSet<string>
        {
            "без", "перед", "при", "через", "над", "об", "под", "про", "для",
            "она", "оно", "они", "себя", "мой", "твой", "свой", "ваш", "наш", "его",
            "кто", "что", "какой", "чей", "где", "который", "откуда", "сколько", "каковой", "каков", "зачем",
            "кто", "что", "какой", "который", "чей", "сколько", "каковой", "каков", "зачем", "когда", "тот", "этот",
            "столько", "такой", "таков", "сей", "всякий", "каждый", "сам", "самый", "любой", "иной", "другой", "весь",
            "никто", "ничто", "никакой", "ничей", "некого", "нечего", "незачем", "некто", "весь", "нечто", "некоторый",
            "несколько", "кто-то", "что-нибудь", "какой-либо"
        };

        public string[] FilterWords(string[] words)
        {
            return words
                .Select(word => word.ToLower())
                .Where(word => word.Length > 2
                               && !stopWords.Contains(word))
                .ToArray();
        }

        public void AddStopWord(string stopWord)
        {
            stopWords.Add(stopWord);
        }
    }
}
