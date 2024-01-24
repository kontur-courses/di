namespace TagsCloudContainer.FrequencyAnalyzers
{
    public class FrequencyAnalyzer : IAnalyzer
    {
        private readonly TextPreprocessing preprocessor;

        private readonly Dictionary<string, int> wordFrequency;
        public FrequencyAnalyzer()
        {
            wordFrequency = new Dictionary<string, int>();
            preprocessor = new TextPreprocessing("excludedWords.txt");
        }

        public void Analyze(string text)
        {
            foreach (var word in preprocessor.Preprocess(text))
            {
                if (wordFrequency.ContainsKey(word.ToLower()))
                {
                    wordFrequency[word]++;
                }
                else
                {
                    wordFrequency.Add(word, 1);
                }
            }
        }

        public IEnumerable<(string, int)> GetAnalyzedText()
        {
            foreach (KeyValuePair<string, int> pair in wordFrequency)
            {
                yield return (pair.Key, pair.Value);
            }
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (KeyValuePair<string, int> pair in wordFrequency)
                {
                    writer.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
        }
    }
}
