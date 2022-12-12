namespace TagCloud.Common.TextFilter;

public interface ITextFilter
{
    public IEnumerable<string> FilterAllWords(string pathToFile, int boringWordsLength);
}