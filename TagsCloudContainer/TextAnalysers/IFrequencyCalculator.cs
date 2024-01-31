namespace TagsCloudContainer.TextAnalysers;

public interface IFrequencyCalculator
{
    public IEnumerable<WordDetails> CalculateFrequency(IEnumerable<WordDetails> wordsDetails);
}