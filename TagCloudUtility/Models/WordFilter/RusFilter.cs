using System.Collections.Generic;

namespace TagCloud.Utility.Models.WordFilter
{
    /// <inheritdoc />
    /// <summary>
    /// Word filter with added russian pronouns and pretexts
    /// </summary>
    public class RusFilter : WordFilter
    {
        public RusFilter(IEnumerable<string> stopWords, int minimalWordLength = 3) : base(new[]
        {
            "без", "перед", "при", "через", "над", "об", "под", "про", "для",
            "она", "оно", "они", "себя", "мой", "твой", "свой", "ваш", "наш", "его",
            "кто", "что", "какой", "чей", "где", "который", "откуда", "сколько", "каковой", "каков", "зачем",
            "чей", "каков", "зачем", "когда", "тот", "этот",
            "столько", "такой", "таков", "сей", "всякий", "каждый", "сам", "самый", "любой", "иной", "другой", "весь",
            "никто", "ничто", "никакой", "ничей", "некого", "нечего", "незачем", "некто", "весь", "нечто", "некоторый",
            "несколько", "кто-то", "что-нибудь", "какой-либо"
        }, minimalWordLength)
        {
            foreach (var stopWord in stopWords)
            {
                Add(stopWord);
            }
        }
    }
}