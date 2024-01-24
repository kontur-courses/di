namespace TagCloud.WordRanker;

public interface IWordRanker
{
    IEnumerable<(string word, int rank)> RankWords(IEnumerable<string> words);
}