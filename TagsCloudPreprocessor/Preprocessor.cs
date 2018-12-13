using System.Collections.Generic;
using System.Linq;

namespace TagsCloudPreprocessor
{
    public class Preprocessor:IPreprocessor
    {
        private readonly IWordsValidator wordsValidator;
        private readonly ITextParser parser;
        private readonly IFileReader fileReader;

        public Preprocessor(IWordsValidator wordsValidator, ITextParser parser, IFileReader fileReader)
        {
            this.wordsValidator = wordsValidator;
            this.parser = parser;
            this.fileReader = fileReader;
        }

        public IEnumerable<(string Word, int CountInText)> GetValidWordsWithCount(string path, int count)
        {
            var words = wordsValidator.GetValidWords(
                parser.GetWords(
                        fileReader.ReadFromFile(path)));
            
            return GetFrequencyDictionary(words)
                .OrderBy(pair => pair.Value)
                .Reverse()
                .Select(pair => (pair.Key, pair.Value))
                .Take(count);
        }
        
        private Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 1;
                frequencyDictionary[word]++;
            }

            return frequencyDictionary;
        }
    }
}