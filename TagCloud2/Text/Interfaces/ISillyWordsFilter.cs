namespace TagCloud2
{
    public interface ISillyWordsFilter
    {
        string FilterSillyWords(string input, ISillyWordSelector selector);
    }
}
