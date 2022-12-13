namespace TagCloud.Common.TextFilter;

public interface ITextFilter
{
    public IEnumerable<string> FilterAllWords(IEnumerable<string> lines, int boringWordsLength);
}