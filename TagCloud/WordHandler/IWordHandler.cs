namespace TagCloud.WordHandler;

public interface IWordHandler
{
    IEnumerable<(string word, int rank)> RankWords(IEnumerable<string> words);
}