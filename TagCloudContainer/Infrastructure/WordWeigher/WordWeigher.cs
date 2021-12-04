namespace TagCloudContainer.Infrastructure.WordWeigher;

public class WordWeigher : IWordWeigher
{
    private readonly ILemmatizer lemmatizer;

    public WordWeigher(ILemmatizer lemmatizer)
    {
        this.lemmatizer = lemmatizer;
    }

    public ICollection<WeightedWord> GetWeightedWords(IEnumerable<string> lines)
    {
        var wordWithWeight = new Dictionary<string, int>();

        foreach (var line in lines)
        {
            var word = line.Trim().ToLower();

            if (!lemmatizer.TryLemmatize(word, out var lemma))
                continue;

            if (wordWithWeight.ContainsKey(lemma))
                wordWithWeight[lemma]++;
            else
                wordWithWeight.Add(lemma, 1);
        }

        return wordWithWeight
            .OrderBy(x => x.Value)
            .Select(x => new WeightedWord(x.Key, x.Value))
            .ToList();
    }
}