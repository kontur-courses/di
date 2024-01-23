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

            for (int i = 0; i < interestingWords.Count; i++)
            {
                if (!wordFrequencies.ContainsKey(interestingWords[i]))
                    wordFrequencies.Add(interestingWords[i], 0);
                wordFrequencies[interestingWords[i]]++;
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
