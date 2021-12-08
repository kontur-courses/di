namespace TagCloud.Infrastructure.Lemmatizer;

public class Lemma
{
    public string Word { get; }
    public PartOfSpeech PartOfSpeech { get; }

    public Lemma(string word, PartOfSpeech partOfSpeech)
    {
        Word = word;
        PartOfSpeech = partOfSpeech;
    }
}