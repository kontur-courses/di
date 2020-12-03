using System;
using System.Linq;

namespace TagsCloudVisualisation.Text.Preprocessing
{
    [VisibleName("Only with length more or equal to 3")]
    public class LengthWordFilter : IWordFilter
    {
        public bool IsValidWord(string word) => word.Length >= 3;
    }

    [VisibleName("Without blacklisted words")]
    public class BlacklistWordFilter : IWordFilter
    {
        private static readonly string[] blacklistedWords =
        {
            "В", "БЕЗ", "ДО", "ДЛЯ", "ЗА", "ЧЕРЕЗ", "НАД", "ПО", "ИЗ", "У", "ОКОЛО", "ПОД", "О", "ПРО", "НА", "К",
            "ПЕРЕД", "ПРИ", "С", "МЕЖДУ", "КАК", "ЧТО", "ГДЕ", "НЕ", "НИ", "ВОВСЕ", "ВОТ", "ЭТО"
        };

        public bool IsValidWord(string word) =>
            !blacklistedWords.Any(w => w.Equals(word, StringComparison.CurrentCultureIgnoreCase));
    }

    [VisibleName("Without swearing")]
    public class SwearingWordFilter : IWordFilter
    {
        private static readonly string[] swearingRoots =
        { 
            "хуй", "пизд", "пизд", "бля", "епт", "ёпт", "ебат", "ёба", "ёбы", "елда"
        }; //sry for this

        public bool IsValidWord(string word) =>
            !swearingRoots.Any(r => word.Contains(r, StringComparison.CurrentCultureIgnoreCase));
    }
}