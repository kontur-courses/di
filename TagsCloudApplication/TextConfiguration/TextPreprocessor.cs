using System.Collections.Generic;
using System.Linq;

namespace TextConfiguration
{
    public class TextPreprocessor
    {
        private readonly ITextPreprocessingSettings settings;

        public TextPreprocessor(ITextPreprocessingSettings settings)
        {
            this.settings = settings;
        }

        public Dictionary<string, int> PreprocessText(string text)
        {
            var preprocessedWords = new Dictionary<string, int>();

            var words = text.Split();
            foreach (var word in words)
                if (word.Length != 0 && settings.TryPreprocessWord(word, out var preprocessedWord))
                {
                    if (!preprocessedWords.ContainsKey(preprocessedWord))
                        preprocessedWords[preprocessedWord] = 0;
                    preprocessedWords[preprocessedWord]++;
                }

            return preprocessedWords;
        }
    }
}
