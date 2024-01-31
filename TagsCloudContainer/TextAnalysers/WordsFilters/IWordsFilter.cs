namespace TagsCloudContainer.TextAnalysers.WordsFilters;

public interface IWordsFilter
{
    public IEnumerable<WordDetails> Filter(IEnumerable<WordDetails> wordDetails);
}