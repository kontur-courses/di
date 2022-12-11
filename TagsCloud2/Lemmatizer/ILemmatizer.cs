namespace TagsCloud2;

public interface ILemmatizer
{
    public List<string> Lemmatize(List<string> words);
}