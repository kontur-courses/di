namespace TagCloudDi.TextProcessing
{
    public class TextProcessor
    {
        public IReadOnlyDictionary<string, int> Words { get; } 
        
        public TextProcessor(Settings settings, TextReader textReader)
        {
            var excludedWords = textReader.GetWordsFrom(settings.ExcludedWordsPath).ToHashSet();
            Words = textReader.GetWordsFrom(settings.TextPath)
                .Where(t => t.Length > 3 && !excludedWords.Contains(t))
                .GroupBy(x => x)
                .ToDictionary(key => key.Key, amount => amount.Count());
        }
    }
}
