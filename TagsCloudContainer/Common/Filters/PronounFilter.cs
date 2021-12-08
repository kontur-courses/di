using System.Collections.Generic;
using TagsCloudContainer.Common.Contracts;

namespace TagsCloudContainer.Common.Filters
{
    public class PronounFilter : IWordFilter
    {
        private readonly HashSet<string> pronouns = new HashSet<string>
        {
            "я", "мы", "ты", "вы", "он", "она", "оно", "они",
            "себя",
            "мой", "твой", "ваш", "наш", "свой", "его", "ее", "их",
            "то", "это", "тот", "этот", "такой", "таков", "столько",
            "весь", "всякий", "сам", "самый", "каждый", "любой", "иной", "другой",
            "кто", "что", "какой", "каков", "чей", "сколько",
            "никто", "ничто", "некого", "нечего", "никакой", "ничей", "нисколько",
            "некто", "нечто", "некоторый", "некий"
        };

        private readonly HashSet<string> pronounPrefixes = new HashSet<string>
        {
            "кое"
        };

        private readonly HashSet<string> pronounSuffixes = new HashSet<string>
        {
            "то", "нибудь", "либо"
        };

        public bool IsValid(string word)
        {
            if (pronouns.Contains(word))
                return false;

            var complexWord = word.Split('-');
            if (complexWord.Length == 2 &&
                (pronounPrefixes.Contains(complexWord[0]) ||
                pronounSuffixes.Contains(complexWord[1])))
                return false;

            return true;
        }
    }
}