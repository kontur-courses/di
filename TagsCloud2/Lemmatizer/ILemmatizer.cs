namespace TagsCloud2.Lemmatizer;

public interface ILemmatizer
{
    public List<string> Lemmatize(List<string> words);
}