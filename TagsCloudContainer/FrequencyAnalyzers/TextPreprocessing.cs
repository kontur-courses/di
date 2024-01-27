namespace TagsCloudContainer.FrequencyAnalyzers
{
    public class TextPreprocessing
    {
        private readonly HashSet<string> excludedWords = new HashSet<string>();
        public TextPreprocessing(string excludedWordsPath)
        {
            if (File.Exists(excludedWordsPath))
            {
                StreamReader reader = new StreamReader(excludedWordsPath);
                excludedWords = reader.ReadToEnd()
                    .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToHashSet();
            }
        }
        public IEnumerable<string> Preprocess(string text)
        {
            var words = text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToLower());
            foreach (var word in words)
            {
                if (!excludedWords.Contains(word))
                    yield return word;
            }
        }
    }
}
