namespace TagsCloudVisualization.ToTagConverter;

public class WordToTagTransformer : IToTagConverter
{
    private readonly Dictionary<string, int> weights = new Dictionary<string, int>();

    public IEnumerable<Tag> Convert(IEnumerable<string> words)
    {
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
        }

        return weights.Select(x => new Tag(x.Key, x.Value));
    }
}