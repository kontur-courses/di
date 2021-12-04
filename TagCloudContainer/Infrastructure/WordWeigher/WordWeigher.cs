namespace TagCloudContainer.Infrastructure.WordWeigher;

public class WordWeigher : IWordWeigher
{
    private readonly ILemmatizer lemmatizer;

    public WordWeigher(ILemmatizer lemmatizer)
    {
        this.lemmatizer = lemmatizer;
    }

    public IEnumerable<WeightedWord> GetWeightedWords(IEnumerable<string> lines)
    {
        var wordWithWeight = new Dictionary<string, int>();

        foreach (var line in lines)
        {
            if (!lemmatizer.TryLemmatize(line.ToLower(), out var lemma))
                continue;

            if (wordWithWeight.ContainsKey(lemma))
                wordWithWeight[lemma]++;
            else
                wordWithWeight.Add(lemma, 1);
        }

        return wordWithWeight
            .OrderBy(x => x.Value)
            .Select(x => new WeightedWord(x.Key, x.Value));
    }
}