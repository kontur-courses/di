namespace TagsCloudVisualization.Words;

public class CustomWordSizeCalculator : IWordsSizeCalculator
{
    public Dictionary<string, float> CalcSizeForWords(Dictionary<string, int> wordsAndCount, float minFontSize, float maxFontSize)
    {
        if (minFontSize < 1)
            throw new ArgumentException("MinFontSize should be greater than 1");

        if (maxFontSize <= minFontSize)
            throw new ArgumentException("maxFontSize should be greater than MinFontSize");

        var wordsFrequency = wordsAndCount.Values.ToList();

        var groupByUniqueCount = wordsFrequency.GroupBy(r => r)
            .Select(r => r.Key)
            .OrderBy(r => r)
            .ToList();

        var sizesDiff = maxFontSize - minFontSize;
        var step = sizesDiff / groupByUniqueCount.Count;
        var currentSize = minFontSize;

        var sizeForFrequency = new Dictionary<int, float>();
        foreach (var oneWordCount in groupByUniqueCount)
        {
            sizeForFrequency[oneWordCount] = currentSize;
            currentSize += step;
        }

        var sizesForWords = new Dictionary<string, float>();
        foreach (var group in wordsAndCount)
        {
            if (!sizeForFrequency.TryGetValue(group.Value, out var wordSize))
                wordSize = minFontSize;

            sizesForWords.Add(group.Key, wordSize);
        }

        return sizesForWords;
    }
}