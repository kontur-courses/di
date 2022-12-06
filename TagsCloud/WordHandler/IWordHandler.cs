namespace TagsCloud.WordHandler;

public interface IWordHandler
{
    public string[] ProcessWords(IEnumerable<string> words);
    public string? ProcessWord(string word);
}