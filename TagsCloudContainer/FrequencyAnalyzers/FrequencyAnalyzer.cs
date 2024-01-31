namespace TagsCloudContainer.FrequencyAnalyzers
{
    public class FrequencyAnalyzer : IAnalyzer
    {
        private readonly Dictionary<string, int> wordFrequency;

        public FrequencyAnalyzer()
        {
            wordFrequency = new Dictionary<string, int>();
        }

        public void Analyze(string text, string exclude = "")
        {
            var preprocessor = new TextPreprocessing(exclude);

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
            return wordFrequency.Select(p => (p.Key, p.Value));
        }
    }
}