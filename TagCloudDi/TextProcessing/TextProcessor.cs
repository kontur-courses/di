namespace TagCloudDi.TextProcessing
{
    public class TextProcessor(Settings settings, TextReader textReader)
    {
        public Dictionary<string, int> GetWordsFrequency()
        {
            var excludedWords = textReader.GetWordsFrom(settings.ExcludedWordsPath).ToHashSet();
            return textReader.GetWordsFrom(settings.TextPath)
                .Where(t => t.Length > 3 && !excludedWords.Contains(t))
                .GroupBy(x => x)
                .ToDictionary(key => key.Key, amount => amount.Count());
        }
    }
}
