namespace TagCloudCreator.Interfaces;

public interface IWordsFilter
{
    public IEnumerable<string> FilterWords(IEnumerable<string> sourceWords);
}