namespace TagCloud.Domain.WordProcessing.Interfaces;

public interface IWordExtractor
{
    public bool IsSuitable(string word);
}