using System.Collections.Generic;
using System.Linq;

namespace TagsCloudPreprocessor
{
    public class Preprocessor:IPreprocessor
    {
        private readonly IWordsValidator wordsValidator;
        private readonly IReader reader;
        private readonly ITextParser parser;
        private readonly IFileReader fileReader;

        public Preprocessor(IWordsValidator wordsValidator, IReader reader, ITextParser parser, IFileReader fileReader)
        {
            this.wordsValidator = wordsValidator;
            this.reader = reader;
            this.parser = parser;
            this.fileReader = fileReader;
        }

        public IEnumerable<string> GetValidWords(string path, int count)
        {
            return wordsValidator.GetValidWords(
                parser.GetWords(
                    reader.GetTextFromRawFormat(
                        fileReader.ReadFromFile(path)))).Take(count);
        }
    }
}