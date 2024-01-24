namespace TagCloudDi.TextProcessing
{
    public class TextProcessor
    {
        public IReadOnlyList<string> Words { get; } 
        
        public TextProcessor(Settings settings, TextReader textReader)
        {
            var excludedWords = textReader.GetWordsFrom(settings.ExcludedWordsPath).ToHashSet();
            Words = textReader.GetWordsFrom(settings.TextPath)
                .Where(t => t.Length > 3 && !excludedWords.Contains(t))
                .ToList()
                .AsReadOnly();
        }
    }
}
