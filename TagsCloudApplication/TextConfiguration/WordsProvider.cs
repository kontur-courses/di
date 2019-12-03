using System.Collections.Generic;
using TextConfiguration.TextReaders;

namespace TextConfiguration
{
    public class WordsProvider
    {
        private readonly ITextReader reader;
        private readonly TextPreprocessor preprocessor;

        public WordsProvider(ITextReader reader, TextPreprocessor preprocessor)
        {
            this.reader = reader;
            this.preprocessor = preprocessor;
        }

        public List<string> ReadWordsFromFile(string filePath)
        {
            var text = reader.ReadText(filePath);

            return preprocessor.PreprocessText(text);
        }
    }
}
