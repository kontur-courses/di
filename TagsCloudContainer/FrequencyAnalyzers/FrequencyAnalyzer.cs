namespace TagsCloudContainer.FrequencyAnalyzers
{
    public class FrequencyAnalyzer : IAnalyzer
    {
        private readonly Dictionary<string, int> wordFrequency;

        public FrequencyAnalyzer()
        {
            wordFrequency = new Dictionary<string, int>();
        }

        public void Analyze(string text) // TODO: filter booring words
        {
            string[] words = text.Split(new[] { ' ', '.', ',', ';', ':', '!', '?', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                if (wordFrequency.ContainsKey(word.ToLower()))
                {
                    wordFrequency[word.ToLower()]++;
                }
                else
                {
                    wordFrequency.Add(word.ToLower(), 1);
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
