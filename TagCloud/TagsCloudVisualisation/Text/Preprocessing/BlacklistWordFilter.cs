using System;
using System.Linq;

namespace TagsCloudVisualisation.Text.Preprocessing
{
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
}