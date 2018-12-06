using System.Collections.Generic;

namespace TagsCloudContainer
{
    internal class WordsCustomizer
    {
        public string CustomizeWord(string word)
        {
            if (IgnoreWord(word))
                return null;

            word = word.ToLower();
            return ToBaseForm(word);
        }

        public bool IgnoreWord(string word)
        {
            // Место для исключения скучных слов, потенциального исключения определенных частей речи
            var wordsToIgnore = new List<string>() {"a", "an", "the", "he", "on", "at", "to"};

            return wordsToIgnore.Contains(word.ToLower());
        }

        private string ToBaseForm(string word)
        {
            // Место для потенциального приведения слов к начальной форме
            return word;
        }
    }
}
