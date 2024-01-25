using StopWord;

namespace TagsCloudContainer.Algorithm
{
    public class WordProcessor : IWordProcessor
    {

        private readonly IFileParser parser;

        public WordProcessor(IFileParser parser)
        {
            this.parser = parser;
        }

        public Dictionary<string, int> CalculateFrequencyInterestingWords(string sourceFilePath, string boringFilePath)
        {
            var wordFrequencies = new Dictionary<string, int>();
            var interestingWords = GetInterestingWords(sourceFilePath, boringFilePath);

            foreach(var word in interestingWords)
            {
                if (!wordFrequencies.ContainsKey(word))
                    wordFrequencies.Add(word, 0);
                wordFrequencies[word]++;
            }

            return wordFrequencies.OrderByDescending(x => x.Value).ToDictionary();
        }

        public List<string> GetInterestingWords(string sourceFilePath, string boringFilePath)
        {
            var boringWords = parser.ReadWordsInFile(boringFilePath);
            var sourceWords = parser.ReadWordsInFile(sourceFilePath);
            var stopWords = StopWords.GetStopWords("ru");

            var interestingWords = sourceWords.Where(word => !boringWords.Contains(word) && !stopWords.Contains(word)).ToList();

            return interestingWords;
        }
    }
}
