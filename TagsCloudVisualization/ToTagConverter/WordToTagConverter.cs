namespace TagsCloudVisualization.ToTagConverter;

public class WordToTagConverter : IToTagConverter
{
    private readonly Dictionary<string, int> weights = new Dictionary<string, int>();

    public IEnumerable<Tag> Convert(IEnumerable<string> words)
    {
        var maxCount = 1;
        foreach (var word in words)
        {
            if (weights.ContainsKey(word))
            {
                weights[word]++;
            }
            else
            {
                weights.Add(word, 1);
            }

            maxCount = Math.Max(maxCount, weights[word]);
        }

        return weights.Select(x => new Tag(x.Key, x.Value / (float)maxCount))
            .OrderByDescending(x => x.Weight);
    }
}