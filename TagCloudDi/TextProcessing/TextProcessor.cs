namespace TagCloudDi.TextProcessing
{
    public class TextProcessor(Settings settings, ITextReader fileTextReader) : ITextProcessor
    {
        public Dictionary<string, int> GetWordsFrequency()
        {
            var excludedWords = fileTextReader.GetWordsFrom(settings.ExcludedWordsPath).ToHashSet();
            return fileTextReader.GetWordsFrom(settings.TextPath)
                .Where(t => t.Length > 3 && !excludedWords.Contains(t))
                .GroupBy(x => x)
                .ToDictionary(key => key.Key, amount => amount.Count());
        }
    }
}
